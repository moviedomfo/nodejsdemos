using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pelsoft.Log.Common
{
    public class AccountsInstancesServicesBE 
    {
        public AccountsInstancesServicesBE()
        {
            this.Proxy = new Proxy();
        }

        public Guid AccountDetailUnique { get; set; }
        public string AccountDetailDescrip { get; set; }
        public int Interval { get; set; }
        public string ConnectionString { get; set; }
        public string ISHostName { get; set; }
        public string Ip { get; set; }
        public string LogOnFile { get; set; }

        public Proxy Proxy { get; set; }
        public string ProxyHost { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassword { get; set; }
        public string ProxyDomain { get; set; }

        public string ServiceChannelName { get; set; }
        public Pelsoft.Log.Common.PelsoftEnums.Channeltype ChannelType { get; set; }
        public string AccountName { get; set; }
        public string AsiConnectionStringDest { get;set; }
        public string DataBaseNameDest { get; set; }
        public Int32 DataBaseID { get; set; }

        public Boolean AsiProcessDetailsOnTarget { get; set; }
        public string OriginalTypeName { get; set; }
        public Int32 OriginalTypeId { get; set; }
        public string DataBaseName { get; set; }

        public Guid AccountGroupUnique { get; set; }
        public string AccountGroupDescrip { get; set; }

        public override string ToString()
        {
            return string.Concat("AccountName: ", this.AccountName, "ChannelType: ", ServiceChannelName);
        }

        public Guid AsiDetailUnique { get; set; }

        public AuditMailSetting_Struct AuditMailSetting { get; set; }

        public bool ProcessDetailsOnTarget { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Topic { get; set; }

        public AccountsInstancesServicesBE Clone()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<AccountsInstancesServicesBE>(json);

        }


    }

    public class AuditMailSetting_Struct
    {
        /// <summary>
        /// Determina si se enviara mail o no: Por el momento esta propiedad esta en true
        /// Siempre:
        /// Y el valor False lo asume solo si alguna propiedad de esta clase no esta configurada
        /// </summary>
        public bool Audit_MailSendMail { get; set; }

        public string Audit_MailRecipients { get; set; }

        public string Audit_MailSender { get; set; }

        public string Audit_MailPassword { get; set; }

        public string Audit_MailUserName { get; set; }

        public bool? Audit_MailEnableSSL { get; set; }

        public int? Audit_MailSMPT_PORT { get; set; }

        public string Audit_MailSMTP_SERVER { get; set; }

        public String[] MAIL_Recipients { get; set; }

        Boolean isServiceInstance = false;

        public Boolean IsServiceInstance
        {
            get { return isServiceInstance; }
            set { isServiceInstance = value; }
        }
    }
}