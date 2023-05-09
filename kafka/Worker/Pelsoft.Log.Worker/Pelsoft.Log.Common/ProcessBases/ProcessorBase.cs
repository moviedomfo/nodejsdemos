using System.Net;
using Fwk.Exceptions;
using Fwk.Logging;
using Newtonsoft.Json;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.Workers;
using Confluent.Kafka;

namespace Pelsoft.Log.Common.ProcessBases
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="procId"></param>
    /// <param name="source"></param>
    /// <param name="incidentType"></param>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public delegate void IncidentHandler(Guid? procId, string source, PelsoftEnums.IncidentType incidentType, string message, Exception ex);


    /// <summary>
    /// El proceso se construye automaticamente en estado WaitingForDoWork
    /// Cuando se llama al metodo Start se inicializa el timmer 
    /// En Time elapsed se realizan previos chequeos antes de poner el estado del proceso en Working. Si los chequeos no detectan ningun cambio
    /// de metadatos: El proceso pasa a estado WaitingForDoWork y  DoWork() puede ejecutarce cuando llega un mensaje de la cola
    /// Cuando finaliza DoWork() el proceso pasa a estado WaitingForDoWork nuevamente
    /// 
    /// De moemnto el método start es quien crea instancia _AccountServiceInstance: antes se pasaba en el constructor dado que la instanciacion era dinamica por Reflection
    /// </summary>
    public abstract class ProcessorBase : IProcessor
    {
        #region Properties

        #region kafka properties
         
        protected CancellationTokenSource cancellationTokenSource { get; set; }

        /// <summary>
        /// Cola o flujo de datos sobre un tema en particular, identificados por un nombre
        /// Particiones: forma en la que se dividen los topic al crearlos. 
        /// Mensajes: es cada elemento que se almacena en un topic, son inmutables y se garantiza el orden dentro de una misma partición
        /// </summary>
        protected string Topics { get; set; }

        /// <summary>
        /// Nombre del/los brokers o cada uno de los servicios de kafka 
        /// </summary>
        protected string BootstrapServers { get; set; }

        /// <summary>
        /// Los consumidores se etiquetan a sí mismos con un nombre de grupo de consumidores, y cada registro publicado en un tema se entrega a 
        /// una instancia de consumidor dentro de cada grupo de consumidores suscriptor. Las instancias de consumidor pueden estar en procesos separados o en máquinas separadas.
        /// Si todas las instancias de consumidor tienen el mismo grupo de consumidores, entonces los registros se equilibrarán efectivamente en la carga de las instancias de consumidor
        /// </summary>
        protected string GroupId { get; set; }
        #endregion

        CancellationToken stoppingToken;

        protected readonly IPelsoftLogService LogService;

        protected bool proxyWasChanged = false;

        /// <summary>
        /// Estado en el que se encuentra el proceso 
        ///     WaitingForDoWork : No hay mensajes esta solo en escucha
        ///     Stoped: El proceso está detenido al 100%- Pero ahun permanece en la Pila de ///procesos del Engine
        ///     Working: Se está efectuando tareas dentro de DoWork : Es decir llego  un mensaje de la cola y se esta procesando 
        /// </summary>
        private PelsoftEnums.ProcessStatus _ProcessStatus = PelsoftEnums.ProcessStatus.WaitingForDoWork;

        public PelsoftEnums.ProcessStatus ProcessStatus
        {
            get { return _ProcessStatus; }
            set { _ProcessStatus = value; }
        }

        public int currentProcessDetailsID;
        public Guid ProcessorId { get; set; }
        public DateTime RefreshActiveDate { get; set; }
        public string SourceName { get; set; }//ServiceAccountchannelName;
        public WebProxy Proxy { get; set; }
        public event IncidentHandler IncidentEvent;
        System.Timers.Timer _Timer;

        /// <summary>
        /// Tiempo que el servicio estara detenido
        /// </summary>
        protected int sleepTime = 0;
        /// <summary>
        /// Hora en el que 
        /// </summary>
        DateTime? serviceStopTime;

        AccountsInstancesServicesBE _AccountServiceInstance = null;
        //public string _Status = string.Empty;
        //public string _Error = string.Empty;
        //public string _Response = string.Empty;

        /// <summary>
        /// Account Service Instance 
        /// Se obtienen sus datos desde Get_AccountsInstanceByParams
        /// </summary>
        public AccountsInstancesServicesBE AccountInfo
        {
            get { return _AccountServiceInstance; }
            set { _AccountServiceInstance = value; }
        }



        /// <summary>
        /// Do any business job .-
        /// </summary>
        /// <param name="message">Menssage or value from queue</param>
        /// <param name="topic">Spesific Queue name, This is so since the process listens to more than one queue or topic. </param>
        /// <returns></returns>
        public abstract Task DoWork(string message,string topic);




        /// <summary>
        /// Efectua verificaciones de verificacion de cambio de metadatos desde la BD. Pero 
        /// es independiente y propia de cada cada proceso
        /// </summary>
        public virtual void RefreshProcessData() { }

        /// <summary>
        /// Veridfica cambios enconfiguracion de AccountsInstancesServicesBE.Proxy
        /// </summary>
        /// <param name="accountChannel_old">Anterior _AccountServiceInstance</param>
        /// <returns></returns>
        void Refresh_Proxy(AccountsInstancesServicesBE accountChannel_old)
        {
            proxyWasChanged = false;

            #region  Verificar cambos en proxy

            //Verifica Nulos
            if (Proxy == null && _AccountServiceInstance.Proxy != null ||
                Proxy != null && _AccountServiceInstance.Proxy == null)
            {
                proxyWasChanged = true;
            }

            if (proxyWasChanged == true)
            {
                //Si no hay nulos Verifica los datos en proxy
                proxyWasChanged =
                    _AccountServiceInstance.Proxy.ProxyDomain.Equals(accountChannel_old.Proxy.ProxyDomain) == false ||
                    _AccountServiceInstance.Proxy.ProxyHost.Equals(accountChannel_old.Proxy.ProxyHost) == false ||
                    _AccountServiceInstance.Proxy.ProxyPassword.Equals(accountChannel_old.Proxy.ProxyPassword) == false ||
                    _AccountServiceInstance.Proxy.ProxyPort.Equals(accountChannel_old.Proxy.ProxyPort) == false ||
                    _AccountServiceInstance.Proxy.ProxyUserName.Equals(accountChannel_old.Proxy.ProxyUserName) == false;
            }

            #endregion

            if (proxyWasChanged)
            {
                if (_AccountServiceInstance.Proxy != null)
                {
                    Proxy = null;
                    Proxy = new WebProxy(_AccountServiceInstance.Proxy.ProxyHost, _AccountServiceInstance.Proxy.ProxyPort);

                    Proxy.Credentials = new NetworkCredential(_AccountServiceInstance.Proxy.ProxyUserName, _AccountServiceInstance.Proxy.ProxyPassword, _AccountServiceInstance.Proxy.ProxyDomain);
                }
                else
                {
                    Proxy = null;
                }
            }

        }

        /// <summary>
        /// Actividad base llevada a cavo para verificar cambios de metadatos desde la BD.
        /// Este proceso se llama solo desde la clase base
        /// </summary>
        /// <param name="accountChannel"></param>
        void RefreshData()
        {
            AccountsInstancesServicesBE accountChannel_old = null;
            if (_AccountServiceInstance !=null)
                accountChannel_old = _AccountServiceInstance.Clone();

            if(accountChannel_old == null && _AccountServiceInstance == null)
                {
                    ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
                    OnIncident(PelsoftEnums.IncidentType.FatalError, " AccountServiceInstance no se encuentra configurada");

                    return;
                }

            _AccountServiceInstance = ProcessDAC.Get_AccountsInstanceByParams(_AccountServiceInstance.AccountDetailUnique, _AccountServiceInstance.AsiDetailUnique, CommonWorkers.Instance.ServiceInstanceUnique, _AccountServiceInstance.AccountGroupUnique);

            if (_AccountServiceInstance == null)
            {
                ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
                OnIncident(PelsoftEnums.IncidentType.Warning, "AccountServiceInstance no se encuentra configurada");
             
                return;
            }

            ///Reinicio el timer si cambio
            //if ((this._AccountServiceInstance.Interval * 1000) != _Timer.Interval)
            //{
            //    _Timer.Interval = this._AccountServiceInstance.Interval * 1000;
            //}

            Set_AuditMailSetting();

            Refresh_Proxy(accountChannel_old);
        }

        #endregion


        public ProcessorBase(IPelsoftLogService logService, AccountsInstancesServicesBE accountInfo)
        {
            LogService = logService;
            _AccountServiceInstance = accountInfo;
        }
 
        /// <summary>
        /// Crea (abre) un nuevo proceso
        /// Llama al metodo abstracto DoWork
        /// Cierra Process details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void _Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _Timer.Stop();
        
            
            #region check refresh
            // Lo detenemos para que refrezque data
            ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
            try
            {

                //if (ProcessStatus == PelsoftEnums.ProcessStatus.Stoped)
                //    return;

                //RefreshData();
                //RefreshProcessData();
                OnIncident(PelsoftEnums.IncidentType.Information, "Timer elapsed ");

            }
            catch (PelsoftException ex)
            {
                OnIncident(PelsoftEnums.IncidentType.Error, ex);
                ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
                Stop();
                return;
            }
            catch (Exception ex)
            {
                OnIncident(PelsoftEnums.IncidentType.Error, ex);
                ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
                Stop();
                return;
            }
            finally{
                if(_Timer!=null)
                    _Timer.Start();
            }
            #endregion

        }


    

        /// <summary>
        /// Inicia el timmer interno par del porceso para refrezcar sus parametros configurados en la BD
        /// llama globalmente LogServiceDAC.Retrive_All_AccountsInstances
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <exception cref="TechnicalException"></exception>
        public virtual void Start()
        {
        
            validate();
            Set_AuditMailSetting();

            RefreshActiveDate = DateTime.Now;

            SourceName = string.Concat(
                "AccountName=", AccountInfo.AccountName,
                //";AsiDetailUnique= ", AccountInfo.AsiDetailUnique,
                //";AccountDetailUnique=", AccountInfo.AccountDetailUnique,
                ";ServiceChannelName=", AccountInfo.ServiceChannelName);

            _ProcessStatus = PelsoftEnums.ProcessStatus.WaitingForDoWork;

            InitTimer();

            this.cancellationTokenSource = new CancellationTokenSource();
            stoppingToken = cancellationTokenSource.Token;

            startListeningQueue(stoppingToken);
        }

        //public Person persontAux;

        //void cheKafka_test(string message)
        //{
        //    var person = TrySerialize<Person>(message);
        //    bool personChanged = false;
        //    if (persontAux == null)
        //    {
        //        persontAux = person;
        //        personChanged = false;//first
        //    }
                
        //    //cambio
        //    if(!person.Id.Equals(persontAux.Id))
        //    {
        //        persontAux = person;
        //        personChanged = true;
        //    }
                


        //    if (personChanged)
        //        personChanged = false;

        //}


        async void startListeningQueue(CancellationToken stoppingToken)
        {
            var config = GetConsumerConfig();

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            var topicList = this.Topics.Split(','); //If Topic wast´t created:  Subscribed topic not available: apple: Broker: Unknown topic or partition

            consumer.Subscribe(topicList);

            try
            {

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (this.CheckStopTime() == false)
                        return;

                 
                    try
                    {
                        var res = consumer.Consume(stoppingToken);

                        //cheKafka_test(res.Value);

                        await ProcessMessage(res);
                        //This approach gives “at least once” delivery semantics since the offset corresponding
                        //to a message is only committed after the message has been successfully processed.
                        if (res.Message != null)//if (res.Offset % commitPeriod == 0)
                        {
                            //Permite hacer commit y no volveran a llegar
                            consumer.Commit(res);

                        }
                    }
                    catch (KafkaException e)
                    {
                        string message = $"Commit error: {e.Error.Reason}";
                        Log(message);
                        OnIncident(PelsoftEnums.IncidentType.Error, message);
                    }
                    catch (Exception ex)
                    {
                        Log(ex);
                        OnIncident(PelsoftEnums.IncidentType.Error, ex);
                    }


                }

            }
            catch (Exception ex)
            {
                Log(ex);
                OnIncident(PelsoftEnums.IncidentType.Error, ex);
            }
            consumer.Close();

        }

      
        /// <summary>
        /// Procesa el mensaje llegado de kafka
        /// Realiza llamada al _processor.DoWork para operar sonbre el mensaje
        /// 
        /// De momento hace CreateNewProcess / CloseProcessDetails : Esto debera ser analizado a fondo donde se almacenara esta info y cuando sera llevada a cavo
        /// Ahora con un sistema de mensajeria esto se estaria realizando con cada mensaje y ejecutara muchas transaciones
        /// </summary>
        /// <param name="res">ConsumeResult </param>
         Task ProcessMessage(ConsumeResult<Ignore, string> res)
        {
            return Task.Run(async () =>
            {
                if (res.Message != null)
                {
                    //Este trabajo ahora se hace on demand cuando llega el mensaje de al cola
                    this.ProcessStatus = PelsoftEnums.ProcessStatus.Working;

                    try
                    {

                        //TODO: que pasa si hay error al  CreateNewProcess pero nada dice que se podria continuar con el mensaje
                        // El servicio comienza a ejecutarse. Se registra genera un nuevo Process 
                        //inserta en tabla  Log.ProcessDetails

                        //await this.CreateNewProcess();

                        //Se agrga nuevo metodo que devuelva una tarea
                        await this.DoWork(res.Value, res.Topic);

                        //Al finalizar el trabajo se cierra el process con EndDate
                        //await this.CloseProcessDetails();


                    }
                    catch (PelsoftException e)
                    {
                        //If no Fatal error the process will continue
                        if (!PelsoftException.Process_details_settings_FatalError.Contains(e.ErrorCode))
                        {
                            var serviceStopTime = this.Sleep();

                            //OnIncident(PelsoftEnums.IncidentType.Error, e);
                            //Log(e);

                            return;
                        }
                        throw e;
                    }
                    catch (TechnicalException te)
                    {
                        if (te.ErrorId.Equals("3000"))
                        {
                            var serviceStopTime = this.Sleep();
                            PelsoftException ti = new PelsoftException(string.Concat("detenido hasta ", serviceStopTime.ToLongDateString(), " \r\n"), te);
                            ti.ErrorCode = 3000;
                            Log(ti);
                            return;
                        }

                        throw te;
                    }


                }
            
            });
         

        }

        public virtual void Stop()
        {
            if(cancellationTokenSource!=null)
                this.cancellationTokenSource.Cancel();
            if (_Timer != null)
            {

                _Timer.Dispose();
                _ProcessStatus = PelsoftEnums.ProcessStatus.Stoped;
                _Timer = null;
            }
        }
        public virtual void Restart()
        {
            Stop();
           
            InitTimer();
        }

        public void InitTimer()
        {
            _ProcessStatus = PelsoftEnums.ProcessStatus.WaitingForDoWork;
            if (AccountInfo.Interval > 0)
            {
                _Timer = new System.Timers.Timer(AccountInfo.Interval * 1000);
                _Timer.Elapsed += _Timer_Elapsed;
                _Timer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        void OnIncident(PelsoftEnums.IncidentType incidentType, string msg)
        {
            if (IncidentEvent != null)
                IncidentEvent(ProcessorId, SourceName, incidentType, msg, null);
        }

        void OnIncident(PelsoftEnums.IncidentType incidentType, Exception ex)
        {
            if (IncidentEvent != null)
                IncidentEvent(ProcessorId, SourceName, incidentType, null, ex);
        }

        /// <summary>
        /// Check if STOP TIME is  elapsed : true is not sleeping
        /// 
        /// If serviceStopTime es null true 
        /// If serviceStopTime  > Now Returns false
        /// If Now > serviceStopTime  . Ya paso tiempo y retorna true
        /// </summary>
        /// <returns></returns>
        bool CheckStopTime()
        {

            if (!serviceStopTime.HasValue) return true;


            if (DateTime.Compare(DateTime.Now, serviceStopTime.Value) > 0)
            {
                // STOP TIME is elapsed
                serviceStopTime = null;
                return true;
            }
            else { return false; }
        }

        ConsumerConfig GetConsumerConfig()
        {
            return  new ConsumerConfig
            {
                BootstrapServers = BootstrapServers,//_workerConfig.KafkaConfig.BootstrapServers,
                                                    //Acks = Acks.All,
                GroupId = GroupId,//_workerConfig.KafkaConfig.GROUP_ID,
                AutoOffsetReset = AutoOffsetReset.Latest,
                //SaslMechanism = SaslMechanism.Plain,
                //SecurityProtocol = SecurityProtocol.SaslSsl,
                Acks = Acks.Leader,
                // Disable auto-committing of offsets.  allows you to commit offsets explicitly via the Commit method
                EnableAutoCommit = false,
                //By default, the .NET Consumer will commit offsets automatically. This is done periodically by a background thread at an interval specified by the AutoCommitIntervalMs 
                //An offset becomes eligible to be committed immediately prior to being delivered to the application via the Consume method
                AutoCommitIntervalMs = (int)TimeSpan.FromMinutes(1).TotalMilliseconds,
                MaxPollIntervalMs = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
                SessionTimeoutMs = (int)TimeSpan.FromSeconds(10).TotalMilliseconds,
                EnablePartitionEof = true,

            };
        }

        void validate()
        {

            if (_AccountServiceInstance == null)
                throw new TechnicalException("AccountsInstancesServices no se encuentra configurado para el proceso " + this.SourceName);

            if (this.Topics == null)
            {
                throw new TechnicalException(String.Format("{0} No tiene configurado Topics para conectarse al sistema de mensajería." +
                    " Para kafka un topic es un flujo de datos sobre un tema en particular una cola ", this.SourceName));
            }

            if (this.BootstrapServers == null)
            {
                throw new TechnicalException(String.Format("{0} No tiene configurado BootstrapServers para conectarse al sistema de mensajería." +
                    " Para kafka son un listado separado por coma host:port que representan los Brokers de kafka a conectarse ", this.SourceName));
            }
            if (this.GroupId == null)
            {
                throw new TechnicalException(String.Format("{0} No tiene configurado GroupId para conectarse al sistema de mensajería." +
                    "Todos los clientes que comparten el mismo group.id pertenecen al mismo grupo ", this.SourceName));
            }

        }

        /// <summary>
        /// llama a logService.LogAsync
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="eventType"></param>
        /// <param name="sendMail"></param>
        protected void Log(string msg, EventType eventType = EventType.Information, bool sendMail = false)
        {
            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.Message = msg;
            epironEvent.AsiDetailUnique = AccountInfo.AsiDetailUnique;

            if (AccountInfo.OriginalTypeId.Equals((int)PelsoftEnums.OriginalType.Account))
                epironEvent.OriginalGUID = AccountInfo.AccountDetailUnique;
            else
                epironEvent.OriginalGUID = AccountInfo.AccountGroupUnique;

            epironEvent.POriginalTypeId = AccountInfo.OriginalTypeId;

            epironEvent.Source = SourceName;

            LogService.LogAsync(epironEvent, sendMail, AccountInfo.AuditMailSetting);
        }

        /// <summary>
        /// Llama a logService.LogAsync 
        /// Siempre enviara mail si el hilo está configurado para eso.
        /// </summary>
        /// <param name="ex"></param>
        protected void Log(Exception ex)
        {
            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.AsiDetailUnique = AccountInfo.AsiDetailUnique;
            epironEvent.OriginalGUID = AccountInfo.AccountDetailUnique;
            epironEvent.Source = SourceName;
            if (AccountInfo.OriginalTypeId.Equals((int)PelsoftEnums.OriginalType.Account))
                epironEvent.OriginalGUID = AccountInfo.AccountDetailUnique;
            else
                epironEvent.OriginalGUID = AccountInfo.AccountGroupUnique;

            epironEvent.POriginalTypeId = AccountInfo.OriginalTypeId;
            LogService.LogAsync(epironEvent, ex, AccountInfo.AuditMailSetting.Audit_MailSendMail, AccountInfo.AuditMailSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="eventType"></param>
        /// <param name="sendMail"></param>
        protected void Log_old(string msg, EventType eventType, bool sendMail)
        {

            //Si no envia mail no es necesario chequear AuditMailSetting.Audit_MailSendMail
            if (sendMail == false)
            {
                LogService.LogAsync(SourceName, msg, eventType, false, null);
            }
            else
            {
                LogService.LogAsync(SourceName, msg, eventType, true, AccountInfo.AuditMailSetting);
            }
        }


        void Set_AuditMailSetting()
        {
            //Si _AccountServiceInstance.AuditMailSetting.Audit_MailSendMail viene en false es porque el hilo no envia mailss
            if (_AccountServiceInstance.AuditMailSetting.Audit_MailSendMail)
            {
                //False= si todo esta en NULL
                _AccountServiceInstance.AuditMailSetting.Audit_MailSendMail =
                _AccountServiceInstance.AuditMailSetting.Audit_MailEnableSSL.HasValue ||
                    !string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailPassword) ||
                    !string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailSender) ||
                    _AccountServiceInstance.AuditMailSetting.Audit_MailSMPT_PORT.HasValue ||
                    !string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailSMTP_SERVER) ||
                    !string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailUserName) ||
                    !string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailRecipients);
            }

            if (!string.IsNullOrEmpty(_AccountServiceInstance.AuditMailSetting.Audit_MailRecipients) && _AccountServiceInstance.AuditMailSetting.Audit_MailSendMail)
            {
                List<string> mailRecipientsTempList = _AccountServiceInstance.AuditMailSetting.Audit_MailRecipients.Split(';').ToList();

                for (int i = 0; i <= mailRecipientsTempList.Count - 1; i++)
                {
                    if (Fwk.HelperFunctions.EMailFunctions.MailAddressValidate(mailRecipientsTempList[i]) == false)
                        mailRecipientsTempList.Remove(mailRecipientsTempList[i]);
                }

                _AccountServiceInstance.AuditMailSetting.MAIL_Recipients = mailRecipientsTempList.ToArray();
            }
        }



        /// <summary>
        /// Serializa un json string. si no puedo lograrlo retorna nulo
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="value">message json string</param>
        /// <returns></returns>
        public T? TrySerialize<T>(string value)
        {
            try
            {
                T? obj = JsonConvert.DeserializeObject<T>(value);
                return obj;

            }
            catch
            {
                return default(T?);
            }
        }

        

        /// <summary>
        /// 
        /// </summary>
        public Task CreateNewProcess()
        {
          return  Task.Run(() =>
            {
                currentProcessDetailsID = ProcessDAC.CreateNewProcess(CommonWorkers.Instance.ServiceInstanceUnique, AccountInfo);
            });

        }

        /// <summary>
        /// 
        /// </summary>
        public Task CloseProcessDetails()
        {
            return Task.Run(() =>
            {
                //Al finalizar el trabajo se cierra el process con EndDate
                ProcessDAC.CloseProcessDetails(currentProcessDetailsID, AccountInfo);
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime Sleep()
        {
            serviceStopTime = DateTime.Now.AddMinutes(sleepTime);
            return serviceStopTime.Value;
        }




    }


    public class Person

    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime? GeneratedDate { get; set; }

        public string kafka_Topic { get; set; }

        public string DocNumber { get; set; }
        [JsonIgnore]
        public string FullName
        {
            get
            {
                return FirstName + ", " + Lastname;
            }
        }







        public Person Clone()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Person>(json);
        }
    }
}