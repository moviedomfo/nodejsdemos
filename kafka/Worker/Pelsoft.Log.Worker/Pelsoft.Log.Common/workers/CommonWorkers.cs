using Pelsoft.Log.Common.DAC;
using Pelsoft.Log.Common.Services;
using Fwk.Exceptions;
using Microsoft.Extensions.Options;


namespace Pelsoft.Log.Common.Workers
{


    /// <summary>
    /// 
    /// </summary>
    public  class CommonWorkers
    {

        private readonly ICnnStringService _CnnStringService;
        private readonly IPelsoftLogService _LogService;

        //public  string InstanceGuid = string.Empty;
        
        public static  ServiceInstance_Struct Instance { get; set; }

        private WorkerConfig _WorkerConfig;


        public CommonWorkers(IOptions<WorkerConfig> workerConfig, ICnnStringService cnnStringService, IPelsoftLogService logService)
        {
            _WorkerConfig = workerConfig.Value;

            _CnnStringService = cnnStringService;
            _LogService = logService;

            this.Initialize();

        }



        public  string GetConnectionString(string cnnName)
        {
            if (_CnnStringService.GetCnnString_byName(cnnName) == null)
                throw new TechnicalException("Falta cadena de conexion " + cnnName + " en el server");

            //System.Data.SqlClient.SqlConnection cnn;
            //if (IsEncrypted())
            //    return ISymetriCypher.Dencrypt(_CnnStringService.get_cnnString_byName(cnnName).cnnString);
            //else
            return _CnnStringService.GetCnnString_byName(cnnName);
        }


        //public CommonWorkers(IOptions<WorkerConfig> workerConfig) { }

        /// <summary>
        /// Inicia la instancia del servicio .- Info del server cadenas de conexion etc
        /// </summary>
         void Initialize()
        {
            PelsoftException te;
            ServiceInstance svc_Instance_config;
            if (_WorkerConfig.ServiceInstanceId == null)
            {
                te = new PelsoftException("WorkerConfig no esta configurado",null);
                te.ErrorCode = 1101;
                throw te;
            }
            
            if (string.IsNullOrEmpty(_WorkerConfig.ServiceInstanceId))
            {
                te = new PelsoftException("epiron_instanceId no esta configurado",null);
                te.ErrorCode = 1101;
                throw te;
            }
            //InstanceGuid = _WorkerConfig.PelsoftInstanceId;
            //ServiceName = _WorkerConfig.ServiceName;

            Instance = new ServiceInstance_Struct();
            Instance.AuditMailSetting = new AuditMailSetting_Struct();
            Instance.AuditMailSetting.Audit_MailSendMail = false;
            Instance.Cnnstring_Master = GetConnectionString(_WorkerConfig.CnnstringNameMaster); //ConfigurationManager.ConnectionStrings["epiron"].ConnectionString;

            if (string.IsNullOrEmpty(Instance.Cnnstring_Master))
            {
                te = new PelsoftException(" No se encuentra configurada la cadena de coneccion Nombre = epiron.", null);
                te.ErrorCode = 1100;
                throw te;
            }

            svc_Instance_config = LogServiceDAC.GetSvcLogInstanceMock(Guid.Parse(_WorkerConfig.ServiceInstanceId));
            //svc_Instance_config = LogServiceDAC.GetSvcLogInstance(Guid.Parse(_WorkerConfig.PelsoftInstanceId));
            if (svc_Instance_config == null)
            {
                te = new PelsoftException(String.Format("No se encuentra la instancia en la BD con el GUID= {0}", _WorkerConfig.ServiceInstanceId), null);

                te.ErrorCode = 1102;
                throw te;
            }
            // This code was moved to ServiceFactory class
            //ChannelAssemblyList = LogServiceDAC.RetriveAllChannelAssembly(Instance.ServiceInstanceUnique);

            #region mapeo bd contra estruct
            
            Instance.ServiceName = _WorkerConfig.ServiceName;

            Instance.InstancesServiceId = svc_Instance_config.ServiceInstanceId;
            Instance.ServiceInstanceUnique = svc_Instance_config.ServiceInstanceGuid;
            Instance.HostName = svc_Instance_config.SIHostName;
            Instance.InstanceName = svc_Instance_config.SIName;
            Instance.Ip = svc_Instance_config.SIIp;
            Instance.LogOnFile = svc_Instance_config.SILogOnFile;
            Instance.Interval = svc_Instance_config.SIInterval;
            Instance.AuditMailSetting = svc_Instance_config.AuditMailSetting;

            //Se pone en false para que no envie mails, hasta que se evalue.
            Instance.AuditMailSetting.Audit_MailSendMail = false;

            #region Check e mail settings

            if (String.IsNullOrEmpty(svc_Instance_config.AuditMailSetting.Audit_MailSender))
            {
                _LogService.Log(Instance.ServiceName, "No se encuentran configurados MailSender", Fwk.Logging.EventType.Warning, false,null);
               Instance.AuditMailSetting.Audit_MailSendMail = false;
            }
            else
            {
                if (svc_Instance_config.AuditMailSetting.Audit_MailRecipients != null)
                {
                    try
                    {
                        List<string> mailRecipientsTempList = svc_Instance_config.AuditMailSetting.Audit_MailRecipients.Split(';').ToList();

                        for (int i = 0; i <= mailRecipientsTempList.Count - 1; i++)
                        {
                            if (Fwk.HelperFunctions.EMailFunctions.MailAddressValidate(mailRecipientsTempList[i]) == false)
                                mailRecipientsTempList.Remove(mailRecipientsTempList[i]);
                        }

                       Instance.AuditMailSetting.MAIL_Recipients = mailRecipientsTempList.ToArray();
                    }
                    catch (Exception)
                    {
                        _LogService.Log(Instance.ServiceName, "MailRecipients no tiene el formato correcto", Fwk.Logging.EventType.Warning, false, null);
                       Instance.AuditMailSetting.Audit_MailSendMail = false;
                    }
                }
                else
                {
                    _LogService.Log(Instance.ServiceName, "No se encuentran configurados MailRecipients", Fwk.Logging.EventType.Warning, false, null);
                   Instance.AuditMailSetting.Audit_MailSendMail = false; ;
                }
            }

            if (Instance.AuditMailSetting.Audit_MailSendMail && String.IsNullOrEmpty(svc_Instance_config.AuditMailSetting.Audit_MailSMTP_SERVER))
            {
                _LogService.Log(Instance.ServiceName, "No se encuentran configurado MAIL_SMTP_SERVER", Fwk.Logging.EventType.Warning, false, null);
               Instance.AuditMailSetting.Audit_MailSendMail = false;
            }

            if (Instance.AuditMailSetting.Audit_MailSendMail && !Instance.AuditMailSetting.Audit_MailEnableSSL.HasValue)
            {
                _LogService.Log(Instance.ServiceName, "No se encuentran configurado SI_MAIL_SMPT_PORT", Fwk.Logging.EventType.Warning, false, null);
               Instance.AuditMailSetting.Audit_MailSendMail = false;
            }

            if (Instance.AuditMailSetting.Audit_MailSendMail && String.IsNullOrEmpty(svc_Instance_config.AuditMailSetting.Audit_MailPassword))
            {
                _LogService.Log(Instance.ServiceName, "No se encuentran configurado SI_MAIL_SMPT_PWD", Fwk.Logging.EventType.Warning, false, null);
               Instance.AuditMailSetting.Audit_MailSendMail = false;
            }

            if (Instance.AuditMailSetting.Audit_MailSendMail && String.IsNullOrEmpty(Instance.AuditMailSetting.Audit_MailUserName))
            {
                _LogService.Log(Instance.ServiceName, "No se encuentran configurado SI_MAIL_USERNAME", Fwk.Logging.EventType.Warning, false, null);
               Instance.AuditMailSetting.Audit_MailSendMail = false;
            }

            #endregion
            #endregion
        }



    }
}
