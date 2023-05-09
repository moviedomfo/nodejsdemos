
//using Confluent.Kafka;
//using Pelsoft.Log.Comerce;
//using Pelsoft.Log.Comerce.BE;
//using Pelsoft.Log.Common;
//using Pelsoft.Log.Common.ProcessBases;
//using Pelsoft.Log.Common.Services;
//using Pelsoft.Log.Common.Workers;
//using Fwk.Exceptions;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;

//namespace WorkerServiceKon.workers
//{
//    /// <summary>
//    /// This worker has the responsibility to listen all topic/queue  Products and persons
//    /// </summary>
//    public class PersonWorker : BackgroundService 
//    {
//        //private readonly int commitPeriod = 1;
//        Timer _Timer; 
        

//        //chanel/AccountDetailUnique  : WHATSAPP-
//        //const string TOPICS = "customers,providers";
//        //const string GROUP_ID = "rrhh-site01";

     
//        private readonly ILogger<PersonWorker> _logger;
//        private readonly IProcessor _processor;

//        private WorkerConfig _workerConfig;

//        public PersonWorker( ILogger<PersonWorker> logger, IPelsoftLogService epironLog, IOptions<WorkerConfig> workerConfig, IConfiguration _configuration)
//        {
            
//            _logger = logger;
//            _processor = new PersonsProcessor(epironLog, _configuration);


//            //commitPeriod = (int)TimeSpan.FromSeconds(_workerConfig.KafkaConfig.CommitPeriod).TotalMilliseconds;

//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {

           
//            //Kafka consumer configuration
//            //var config = new ConsumerConfig
//            //{
//            //    BootstrapServers = _workerConfig.KafkaConfig.BootstrapServers,
//            //    //Acks = Acks.All,
//            //    GroupId = GROUP_ID,
//            //    AutoOffsetReset = AutoOffsetReset.Latest,
//            //    //SaslMechanism = SaslMechanism.Plain,
//            //    //SecurityProtocol = SecurityProtocol.SaslSsl,
//            //    Acks = Acks.Leader,
//            //    // Disable auto-committing of offsets.  allows you to commit offsets explicitly via the Commit method
//            //    EnableAutoCommit = false,
//            //    //By default, the .NET Consumer will commit offsets automatically. This is done periodically by a background thread at an interval specified by the AutoCommitIntervalMs 
//            //    //An offset becomes eligible to be committed immediately prior to being delivered to the application via the Consume method
//            //    AutoCommitIntervalMs = (int)TimeSpan.FromMinutes(1).TotalMilliseconds,
//            //    MaxPollIntervalMs = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
//            //    SessionTimeoutMs = (int)TimeSpan.FromSeconds(10).TotalMilliseconds,
//            //    EnablePartitionEof = true,

//            //};

//            //using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

//            //var topicList = TOPICS.Split(',');
//            ////If Topic wast�t created:  Subscribed topic not available: apple: Broker: Unknown topic or partition
//            //consumer.Subscribe(topicList);

//            //try
//            //{
//            //    //while (!stoppingToken.IsCancellationRequested)
//            //    while (true)
//            //    {

//            //        var res = consumer.Consume(stoppingToken);

//            //        ProcessMessage(res);

//            //        //This approach gives �at least once� delivery semantics since the offset corresponding
//            //        //to a message is only committed after the message has been successfully processed.
//            //        if (res.Message != null)//if (res.Offset % commitPeriod == 0)
//            //        {
//            //            try
//            //            {
//            //                //Permite hacer commit y no volveran a llegar
//            //                consumer.Commit(res);
//            //            }
//            //            catch (KafkaException e)
//            //            {
//            //                _logger.LogInformation($"{DateTimeOffset.Now} commit error: {e.Error.Reason}");
//            //            }
//            //        }

//            //    }



//            //}
//            //catch (Exception ex)
//            //{
//            //    _logger.LogInformation("{time} (error), {message}", DateTimeOffset.Now, ex.Message);
//            //}
//            //consumer.Close();

//            #region  This code is only for BackgroundService demo
//            //while (!stoppingToken.IsCancellationRequested)
//            //{
//            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

//            //    await Task.Delay(1000, stoppingToken);
//            //}
//            #endregion
//        }

//        /// <summary>
//        /// Procesa el mensaje acorde las reglas de negocio . Probablemente solo llame al IProccesor y efectue algunos logs en consola
//        /// </summary>
//        /// <param name="res">ConsumeResult proveniente de kafka</param>
//        //async void ProcessMessage(ConsumeResult<Ignore, string> res)
//        //{
//        //    if (res.Message != null)
//        //    {
                


//        //        string log;
//        //        //Este codigo solo es para debug y testing. No debe ser incluido en un worker de konecta
//        //        var person = JsonConvert.DeserializeObject<PersonBE>(res.Message.Value);
//        //        if (person != null)
//        //        {
//        //            //Este codigo SI es de un worker de konecta
//        //            await _processor.DoWork(res.Message.Value);

//        //            person.kafka_Topic = res.Topic;
//        //            log = string.Format("person arrived to {0} -> {1} ", person.kafka_Topic, person.FullName);
//        //        }
//        //        else
//        //        {
//        //            log = string.Format("{0} message arrived ", res.Message.Value);

//        //        }

//        //        _logger.LogInformation($"{DateTimeOffset.Now} : {log}");
//        //    }

//        //}


//        //async void ProcessMessageTest()
//        //{

//        //    //Este trabajo ahora se hace on demand cuando llega el mensaje de al cola
//        //    _processor.ProcessStatus = PelsoftEnums.ProcessStatus.Working;

//        //    try
//        //    {
//        //        if (_processor.CheckStopTime())
//        //        {
//        //            //El servicio comienza a ejecutarse. Se registra genera un nuevo Process 
//        //            //inserta en tabla  Log.ProcessDetails
//        //            _processor.CreateNewProcess();


//        //            //Se agrga nuevo metodo que devuelva una tarea
//        //            await _processor.DoWork(string.Empty);

//        //            //Al finalizar el trabajo se cierra el process con EndDate
//        //            _processor.CloseProcessDetails();
//        //        }

//        //    }
//        //    catch (TechnicalException te)
//        //    {
//        //        if (te.ErrorId.Equals("3000"))
//        //        {
//        //            var serviceStopTime = _processor.Sleep();
//        //            PelsoftException ti = new PelsoftException(string.Concat("detenido hasta ", serviceStopTime.ToLongDateString(), " \r\n"), te);
//        //            ti.ErrorCode = "3000";
//        //            logError(ti);
//        //            return;
//        //        }


//        //        return;
//        //    }

//        //    catch (Exception ex)
//        //    {
//        //        logError(ex);
//        //    }
//        //    finally
//        //    {
//        //        _processor.Restart(this.);
//        //    }




//        //}

//        /// <summary>
//        /// Calls ExecuteAsync 
//        /// </summary>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
            
//            AccountsInstancesServicesBE accountsInstancesServicesBE = new AccountsInstancesServicesBE();
//            accountsInstancesServicesBE.AccountDetailDescrip = "AccountDetailDescrip";
//            accountsInstancesServicesBE.AccountDetailUnique = Guid.NewGuid();
//            accountsInstancesServicesBE.AsiDetailUnique = Guid.NewGuid(); ;
//            accountsInstancesServicesBE.AuditMailSetting = new AuditMailSetting_Struct();

//            _processor.Start(cancellationToken,accountsInstancesServicesBE);

//            _Timer = new Timer(timmerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

//            return base.StartAsync(cancellationToken);
//        }

//        public override Task StopAsync(CancellationToken cancellationToken)
//        {
//            _Timer?.Change(Timeout.Infinite, 0);
//            return base.StopAsync(cancellationToken);
//        }


//        /// <summary>
//        /// Only for demo
//        /// </summary>
//        /// <param name="sender"></param>
//        private void timmerElapsed(object sender)
//        {
//            string log = string.Format("Timmer runing {0}", DateTimeOffset.Now.ToString());
            

//            try
//            {
//                LogInformation(log);
//                throw new Exception();
//            }
//            catch (Exception ex)
//            {
//                logError(ex);
//            }


//        }

//        // void LogServiceFactory()
//        //{
//            //Instance = new ServiceInstance_Struct();
//            //Helper.Instance.AuditMailSetting = new AuditMailSetting_Struct();
//            //Helper.Instance.AuditMailSetting.Audit_MailSendMail = false;

//            ///Crea el archivo de logs
//            //Instance.LogFileFullName = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "{0}_LogFile.xml");

//        //}

//        void logError(Exception ex)
//        {
//            _logger.LogError(ex.Message);
//        }
//        void LogInformation(string  msg)
//        {
//            _logger.LogInformation(msg);
//        }
//    }
//}