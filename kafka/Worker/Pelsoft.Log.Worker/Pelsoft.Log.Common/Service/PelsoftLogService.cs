using Pelsoft.Log.Common.Data;
using Pelsoft.Log.Common.Workers;
using Fwk.Exceptions;
using Fwk.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Pelsoft.Log.Common.Services
{
    public class PelsoftLogService : IPelsoftLogService
    {
        private readonly ILogger<PelsoftLogService> _Logger;
        private readonly IConfiguration _Configuration;
        private readonly WorkerConfig _WorkerConfig;
        String email_log_body = "Email_Log.htm";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerConfig"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public PelsoftLogService(IOptions<WorkerConfig> workerConfig, 
            IConfiguration configuration, 
            ILogger<PelsoftLogService> logger)
        {
            _WorkerConfig = workerConfig.Value;
            _Configuration = configuration;
            _Logger = logger;
        }

      

        /// <summary>
        /// Store log record in database
        /// </summary>
        /// <param name="pEvent"></param>
        void Insert(PelsoftEvent pEvent)
        {
            var cnnString = _Configuration.GetConnectionString("epiron");
            DBManager.ExecuteStoreProcedure(cnnString,
             (cnn, cmd) =>
             {
                 cmd.CommandText = "fwk_Logs_i";

                 cmd.Parameters.AddWithValue("@Id", pEvent.Id);
                 cmd.Parameters.AddWithValue("@Message", pEvent.Message);
                 cmd.Parameters.AddWithValue("@Source", pEvent.Source);
                 cmd.Parameters.AddWithValue("@LogType", pEvent.LogType);
                 cmd.Parameters.AddWithValue("@Machine", pEvent.Machine);
                 cmd.Parameters.AddWithValue("@LogDate", pEvent.LogDate);
                 cmd.Parameters.AddWithValue("@UserLoginName", pEvent.User);
                 cmd.Parameters.AddWithValue("@AppId", pEvent.AppId);
                 cmd.Parameters.AddWithValue("@ServiceInstanceUnique", pEvent.ServiceInstanceUnique);
                 cmd.Parameters.AddWithValue("@OriginalGUID", pEvent.OriginalGUID);
                 cmd.Parameters.AddWithValue("@POriginalTypeId", pEvent.POriginalTypeId);
                 cmd.Parameters.AddWithValue("@AsiDetailUnique", pEvent.AsiDetailUnique);
                 cmd.Parameters.AddWithValue("@PelsoftLogId", pEvent.PelsoftLogId);

                 cmd.ExecuteNonQuery();
             },
             (ex) =>
             {
                 TechnicalException te = new TechnicalException("Error de Fwk.Logging al intentar insertar log en base de datos", ex);
                 te.ErrorId = "9004";
                 ExceptionHelper.SetTechnicalException<PelsoftLogService>(te);
                 throw te;
             });
        }



        #region Log audit PelsoftEvent

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epironEvent"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting"></param>
        /// <returns></returns>
        public Task  LogAsync(PelsoftEvent epironEvent, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
            return Task.Run(() =>
            {
                Log(epironEvent, sendMail, auditMailSetting);
            });

        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="epironEvent"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting">Si es un proceso y Audit_MailSendMail=false, se intentara enviar desde el la instancia del servicio. (configuracion en la master)</param>
        public void Log(PelsoftEvent epironEvent, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
            #region Crea un Log Objeto

            epironEvent.Machine = Environment.MachineName;
            epironEvent.AppId = CommonWorkers.Instance.ServiceName;
            epironEvent.LogDate = DateTime.Now;
            epironEvent.User = Environment.UserName;
            epironEvent.ServiceInstanceUnique = CommonWorkers.Instance.ServiceInstanceUnique;

            #endregion

            try
            {

                if (_WorkerConfig.PerformLog)
                    Insert(epironEvent);
                //else
                //    Log_NonDatabase(epironEvent);//PAra el caso donde ya no se puede almacenar en BD el error

            }
            catch (Exception ex)//Puede q se llene el log de windows
            {
                this.Logger_logError(ex);
            }

            ///TODO: migration -> review SendMail 
            //Si Instance.SendMail =false es por que no esta configurado las opciones de envio en Master.ServiceInstance
            //if (sendMail & auditMailSetting != null)
            //    SendMail(epironEvent, auditMailSetting);

        }



        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="epironEvent"></param>
        /// <param name="ex"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting"></param> 
        public Task LogAsync(PelsoftEvent epironEvent, Exception ex, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
            return Task.Run(() =>
            {
                Log( epironEvent, ex, sendMail, auditMailSetting);
            });
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="epironEvent"></param>
        /// <param name="ex"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting"></param>
        public void Log(PelsoftEvent epironEvent, Exception ex, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
            epironEvent.Message = string.Concat(ExceptionHelper.GetAllMessageException(ex), " ", ex.Source);
            epironEvent.LogType = EventType.Error;
            if (string.IsNullOrEmpty(epironEvent.Source))
                epironEvent.Source = ex.Source;
            Log(epironEvent, sendMail, auditMailSetting);
        }

        #endregion


        #region Log audit Fwk Event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="msg"></param>
        /// <param name="eventType"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting"></param>
        /// <returns></returns>
        public Task LogAsync(string source, string msg, EventType eventType, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
           return Task.Run(() =>
            {
                Log(source, msg, eventType, sendMail, auditMailSetting);
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="msg"></param>
        /// <param name="eventType"></param>
        /// <param name="sendMail"></param>
        /// <param name="auditMailSetting"></param>
        public void Log(string source, string msg, EventType eventType, bool sendMail, AuditMailSetting_Struct auditMailSetting)
        {
            #region Crea un Log Objeto

            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.Source = source;
            epironEvent.Message = msg;
            epironEvent.LogType = eventType;

            #endregion

            Log(epironEvent, sendMail, auditMailSetting);

        }

        /// <summary>
        /// Crea una entrada en el visor de eventos
        /// </summary>
        /// <param name="source"></param>
        /// <param name="msg"></param>
        /// <param name="eventType"></param>
        /// <param name="sendMail">Determina si en este Log se enviara un e-mail </param>
        //public  void Log(string source, string msg, EventType eventType, bool sendMail)
        //{
        //    #region Crea un Log Objeto

        //    PelsoftEvent epironEvent = new PelsoftEvent();
        //    epironEvent.Source = source;
        //    epironEvent.Message = msg;
        //    epironEvent.LogType = eventType;

        //    #endregion

        //    Log(epironEvent, sendMail, Instance.AuditMailSetting);
        //}

        public Task LogAsync(string source, Exception ex, bool sendMail)
        {
            return Task.Run(() =>
            {
                Log(source, ex,  sendMail);
            });

        }

        public void Log(string source, Exception ex, bool sendMail)
        {
            #region Crea un Log Objeto

            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.Source = source;

            #endregion

            Log(epironEvent, ex, sendMail, CommonWorkers.Instance.AuditMailSetting);

        }



        public Task LogAsync(string source, Exception ex, AuditMailSetting_Struct auditMailSetting)
        {

            return Task.Run(() =>
            {
                Log(source, ex,  auditMailSetting);
            });

          
        }
        public void Log(string source, Exception ex, AuditMailSetting_Struct auditMailSetting)
        {
            #region Crea un Log Objeto

            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.Source = source;

            #endregion

            Log(epironEvent, ex, true, auditMailSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="pSourceName"></param>
        /// <param name="pAccountsInstancesServicesBE"></param>
        public  void WriteFwkLog(string pMsg, string pSourceName, AccountsInstancesServicesBE pAccountsInstancesServicesBE)
        {
            PelsoftEvent wPelsoftEvent = new PelsoftEvent();

            wPelsoftEvent.Machine = Environment.MachineName;
            wPelsoftEvent.AppId = CommonWorkers.Instance.ServiceName;
            wPelsoftEvent.LogDate = System.DateTime.Now;
            wPelsoftEvent.User = Environment.UserName;
            wPelsoftEvent.ServiceInstanceUnique = CommonWorkers.Instance.ServiceInstanceUnique;
            wPelsoftEvent.Message = pMsg;
            wPelsoftEvent.AsiDetailUnique = pAccountsInstancesServicesBE.AsiDetailUnique;

            if (pAccountsInstancesServicesBE.OriginalTypeId.Equals((int)PelsoftEnums.OriginalType.Account))
                wPelsoftEvent.OriginalGUID = pAccountsInstancesServicesBE.AccountDetailUnique;
            else
                wPelsoftEvent.OriginalGUID = pAccountsInstancesServicesBE.AccountGroupUnique;

            wPelsoftEvent.POriginalTypeId = pAccountsInstancesServicesBE.OriginalTypeId;

            wPelsoftEvent.Source = pSourceName;

            Insert(wPelsoftEvent);
        }
        #endregion


        #region -- Send mail --
        /// <summary>
        /// Envia el error por mail de acuerdo a las direcciones configuradas.
        /// </summary>
        /// <param name="source">Origen del error</param>
        /// <param name="error">Detalle del error</param>
        public  void SendMail(string source, string message, AccountsInstancesServicesBE pAccountsInstancesServicesBE)
        {

            PelsoftEvent epironEvent = new PelsoftEvent();
            epironEvent.Source = source;
            epironEvent.Message = message;
            epironEvent.Machine = Environment.MachineName;
            epironEvent.AppId = CommonWorkers.Instance.ServiceName;
            epironEvent.LogDate = System.DateTime.Now;
            epironEvent.User = Environment.UserName;
            epironEvent.ServiceInstanceUnique = CommonWorkers.Instance.ServiceInstanceUnique;
            epironEvent.AsiDetailUnique = pAccountsInstancesServicesBE.AsiDetailUnique;

            if (pAccountsInstancesServicesBE.OriginalTypeId.Equals((int)PelsoftEnums.OriginalType.Account))
                epironEvent.OriginalGUID = pAccountsInstancesServicesBE.AccountDetailUnique;
            else
                epironEvent.OriginalGUID = pAccountsInstancesServicesBE.AccountGroupUnique;

            epironEvent.POriginalTypeId = pAccountsInstancesServicesBE.OriginalTypeId;

            SendMail(epironEvent, CommonWorkers.Instance.AuditMailSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="error"></param>
        /// <param name="auditMailSetting"></param>
        public  void SendMail(PelsoftEvent epironEvent, AuditMailSetting_Struct auditMailSetting)
        {
            if (!auditMailSetting.Audit_MailSendMail) return;

            //Si es false se intentara enviar con La configuracion de la master 
            //Puede que AuditMailSetting_Struct = Helper.Instance.AuditMailSetting y las dos comprobaciones seria innesesarias: pero por cuestiones de rapido desarrollo
            //y mator control de esta funcionalidad se realiza aqui
            if (auditMailSetting.IsServiceInstance == false && auditMailSetting.Audit_MailSendMail == false)
            {
                if (CommonWorkers.Instance.AuditMailSetting.Audit_MailSendMail)
                    auditMailSetting = CommonWorkers.Instance.AuditMailSetting;
                else
                    return;
            }

            try
            {
                if (auditMailSetting.Audit_MailSendMail == false) return;

                VerifySendMail(auditMailSetting);

                #region Setting e-mail SmtpClient

                string body = GeneratEmailBody(epironEvent);

                //Crea el nuevo correo electronico con el cuerpo del mensaje y el asutno.
                System.Net.Mail.MailMessage wMailMessage = new System.Net.Mail.MailMessage() { Body = body, Subject = String.Concat("Error epiron :", epironEvent.Source) };

                wMailMessage.IsBodyHtml = true;

                //Asigna el remitente del mensaje de acuerdo a direccion obtenida en el archivo de configuracion.
                wMailMessage.From = new MailAddress(auditMailSetting.Audit_MailSender);

                //Asigna los destinatarios del mensaje de acuerdo a las direcciones obtenidas en el archivo de configuracion.
                foreach (string recipient in auditMailSetting.MAIL_Recipients)
                {
                    wMailMessage.To.Add(new MailAddress(recipient));
                }

                var smtpclient = new SmtpClient(auditMailSetting.Audit_MailSMTP_SERVER, auditMailSetting.Audit_MailSMPT_PORT.Value)
                {

                    Credentials = new NetworkCredential(auditMailSetting.Audit_MailUserName, auditMailSetting.Audit_MailPassword),
                    EnableSsl = auditMailSetting.Audit_MailEnableSSL.Value
                };

                #endregion

                smtpclient.Send(wMailMessage);
            }
            catch (Exception ex)
            {
                //Intenta enviar si y solo si el error se produjo de una cuenta y no del service instance
                if (auditMailSetting.IsServiceInstance == false)
                    SendMail(epironEvent, CommonWorkers.Instance.AuditMailSetting);

                //Loguea la ocurrencia de error tecnico de envio de e-mail
                Log(epironEvent, ex, false, null);
            }
        }

        void VerifySendMail(AuditMailSetting_Struct auditMailSetting)
        {
            if (auditMailSetting.IsServiceInstance) return;

            PelsoftException ex = null;

            if (string.IsNullOrEmpty(auditMailSetting.Audit_MailSender))
                ex = new PelsoftException("Falta MailSender", null);

            if (string.IsNullOrEmpty(auditMailSetting.Audit_MailRecipients))
                ex = new PelsoftException("Falta MailRecipients", null);

            if (string.IsNullOrEmpty(auditMailSetting.Audit_MailPassword))
                ex = new PelsoftException("Falta MailPassword", null);

            if (string.IsNullOrEmpty(auditMailSetting.Audit_MailSMTP_SERVER))
                ex = new PelsoftException("Falta MailSMTP_SERVER", null);

            if (string.IsNullOrEmpty(auditMailSetting.Audit_MailUserName))
                ex = new PelsoftException("Falta MailUserName", null);

            if (auditMailSetting.Audit_MailSMPT_PORT.HasValue == false)
                ex = new PelsoftException("Falta MailSMPT_PORT", null);

            if (auditMailSetting.Audit_MailEnableSSL.HasValue == false)
                ex = new PelsoftException("Falta MailEnableSSL", null);


            if (ex != null)
            {
                ex.ErrorCode = 3005;
                throw ex;
            }
                
        }


        /// <summary>
        /// Genera un html con el epironEvent desde un template
        /// </summary>
        /// <param name="epironEvent"></param>
        /// <returns></returns>
        String GeneratEmailBody(PelsoftEvent epironEvent)
        {
            StringBuilder BODY = new StringBuilder(email_log_body);
            BODY.Replace("$instancename$", CommonWorkers.Instance.InstanceName);
            BODY.Replace("$ServiceInstanceUnique$", epironEvent.ServiceInstanceUnique.ToString());
            BODY.Replace("$source$", epironEvent.Source);
            BODY.Replace("$message$", epironEvent.Message);

            if (epironEvent.AsiDetailUnique.HasValue)
                BODY.Replace("$asidetailunique$", epironEvent.AsiDetailUnique.ToString());
            else
                BODY.Replace("$asidetailunique$", "");

            if (epironEvent.OriginalGUID.HasValue)
                BODY.Replace("$accountdetailunique$", epironEvent.OriginalGUID.ToString());
            else
                BODY.Replace("$accountdetailunique$", "");

            return BODY.ToString();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ex"></param>
        public void Logger_logError(Exception ex, PelsoftEnums.IncidentType type = PelsoftEnums.IncidentType.Error)
        {
            var typename = Enum.GetName(typeof(PelsoftEnums.IncidentType), type);
            var message = String.Format("{0} err-type : {1}, {2}", DateTimeOffset.Now, typename, ex.Message);
            _Logger.LogError(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Logger_logInfo(string msg, PelsoftEnums.IncidentType type = PelsoftEnums.IncidentType.Information)
        {

            _Logger.LogInformation("{time}, {message}", DateTimeOffset.Now, msg);
        }
    }


}
