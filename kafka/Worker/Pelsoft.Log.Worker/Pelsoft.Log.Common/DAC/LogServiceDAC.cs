using Pelsoft.Log.Common.Data;
using Pelsoft.Log.Common.Workers;
using System.Data.SqlClient;

namespace Pelsoft.Log.Common.DAC
{
    public class LogServiceDAC
    {
        public static PelsoftException ManageException(Exception pException)
        {
            PelsoftException te = PelsoftException.ProcessException(pException, typeof(LogServiceDAC));
            te.ErrorCode = 1200;
            return te;

        }
        public static List<ChannelAssembly> RetriveAllChannelAssembly(Guid instanceId)
        {
            return DBManager.ExecuteStoreProcedure(
             CommonWorkers.Instance.Cnnstring_Master,
             (cnn, cmd) =>
             {
                 ChannelAssembly item;
                 List<ChannelAssembly> items = new List<ChannelAssembly>();

                 cmd.CommandText = "dbo.ChannelAssembly_s";
                 using (SqlDataReader wReader = cmd.ExecuteReader())
                 {
                     while (wReader.Read())
                     {
                         item = new ChannelAssembly();
                         item.ChannelType = Convert.ToInt32(wReader["ChannelType"]);
                         item.ActiveFlag = Convert.ToBoolean(wReader["ActiveFlag"]);
                         item.AssemblyStrongName = Convert.ToString(wReader["AssemblyStrongName"]); ;
                         if (item.ChannelType != (int)PelsoftEnums.Channeltype.AttentionQueue)
                             items.Add(item);
                     }
                 }

                 return items;
             },
             (ex) => throw ManageException(ex));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static ServiceInstance GetSvcLogInstance(Guid instanceId)
        {
            return DBManager.ExecuteStoreProcedure<ServiceInstance>(
             CommonWorkers.Instance.Cnnstring_Master,
             (cnn, cmd) =>
             {
                 ServiceInstance item = null;

                 cmd.CommandText = "Config.ServiceInstance_g_ByInstanceId";
                 cmd.Parameters.AddWithValue("@ServiceInstance", instanceId);

                 using (SqlDataReader wReader = cmd.ExecuteReader())
                 {
                     while (wReader.Read())
                     {
                         item = new ServiceInstance();
                         item.ServiceInstanceId = Convert.ToInt32(wReader["ServiceInstanceId"]);
                         item.ServiceInstanceGuid = instanceId;
                         item.SIHostName = wReader["SIHostName"].ToString();
                         item.SIIp = wReader["SIIp"].ToString();
                         item.SIName = wReader["SIName"].ToString();
                         item.SILogOnFile = Convert.ToBoolean(wReader["SILogOnFile"]);
                         item.SIInterval = Convert.ToInt32(wReader["SIInterval"]);
                         item.AuditMailSetting = new AuditMailSetting_Struct();
                         item.AuditMailSetting.IsServiceInstance = true;
                         if (wReader["SSMAIL_Port"] != DBNull.Value)
                             item.AuditMailSetting.Audit_MailSMPT_PORT = Convert.ToInt32(wReader["SSMAIL_Port"]);
                         item.AuditMailSetting.Audit_MailRecipients = wReader["SIMailRecipients"].ToString();
                         item.AuditMailSetting.Audit_MailSMTP_SERVER = wReader["SSMAIL_Server"].ToString();
                         item.AuditMailSetting.Audit_MailSender = wReader["MSMailSender"].ToString();
                         item.AuditMailSetting.Audit_MailPassword = wReader["MSPassword"].ToString();
                         item.AuditMailSetting.Audit_MailUserName = wReader["MSUserName"].ToString();
                         if (wReader["MSEnableSSL"] != DBNull.Value)
                             item.AuditMailSetting.Audit_MailEnableSSL = Convert.ToBoolean(wReader["MSEnableSSL"]);
                     }
                 }

                 return item;
             },
             (ex) => throw ManageException(ex));
        }

        //
        public static ServiceInstance GetSvcLogInstanceMock(Guid instanceId)
        {

            ServiceInstance item = new ServiceInstance();
            item.ServiceInstanceId = 1000;
            item.ServiceInstanceGuid = instanceId;
            item.SIHostName = "localhostTest";
            item.SIIp = "";
            item.SIName = "";
            item.SILogOnFile = false;
            item.SIInterval = 0;
            item.AuditMailSetting = new AuditMailSetting_Struct();
            item.AuditMailSetting.IsServiceInstance = true;

            item.AuditMailSetting.Audit_MailSMPT_PORT = 25;
            item.AuditMailSetting.Audit_MailRecipients = "";
            item.AuditMailSetting.Audit_MailSMTP_SERVER = "";
            item.AuditMailSetting.Audit_MailSender = "rapi@companyrapi.com";
            item.AuditMailSetting.Audit_MailPassword = "";
            item.AuditMailSetting.Audit_MailUserName = "rapi@companyrapi.com";

            item.AuditMailSetting.Audit_MailEnableSSL = false;


            return item;

        }

        public static List<AccountsInstancesServicesBE> Retrive_All_AccountsInstances(Guid instancesServiceGuid)
        {
            return DBManager.ExecuteStoreProcedure(
             CommonWorkers.Instance.Cnnstring_Master,
             (cnn, cmd) =>
             {
                 List<AccountsInstancesServicesBE> list = new List<AccountsInstancesServicesBE>();
                 AccountsInstancesServicesBE item = null;

                 cmd.CommandText = "Config.AccountServiceInstance_s_ByServiceInstance";
                 cmd.Parameters.AddWithValue("@ServiceInstance", instancesServiceGuid);

                 using (SqlDataReader wReader = cmd.ExecuteReader())
                 {
                     while (wReader.Read())
                     {
                         item = new AccountsInstancesServicesBE();

                         item.AsiDetailUnique = Guid.Parse(wReader["AsiDetailUnique"].ToString());
                         if (wReader["AccountDetailUnique"] != DBNull.Value)
                             item.AccountDetailUnique = Guid.Parse(wReader["AccountDetailUnique"].ToString());
                         if (wReader["AccountDetailDescrip"] != DBNull.Value)
                             item.AccountDetailDescrip = wReader["AccountDetailDescrip"].ToString();
                         item.Interval = Convert.ToInt32(wReader["AsiDetailInterval"]);
                         item.AccountName = wReader["AccountName"].ToString();
                         item.ServiceChannelName = wReader["ChannelName"].ToString();
                         if (wReader["ProxyHost"] != DBNull.Value)
                         {
                             item.Proxy.ProxyHost = wReader["ProxyHost"].ToString();
                             item.Proxy.ProxyUserName = wReader["ProxyUserName"].ToString();
                             item.Proxy.ProxyPassword = wReader["ProxyPassword"].ToString();
                             item.Proxy.ProxyDomain = wReader["ProxyDomain"].ToString();
                             item.Proxy.ProxyPort = Convert.ToInt32(wReader["ProxyPort"]);
                         }
                         else
                             item.Proxy = null;
                         item.ConnectionString = wReader["AsiConnectionString"].ToString();
                         item.ChannelType = (PelsoftEnums.Channeltype)Convert.ToInt32(wReader["ChannelType"]);
                         item.AuditMailSetting = new AuditMailSetting_Struct();
                         if (wReader["AsidetSSMAIL_Port"] != DBNull.Value)
                             item.AuditMailSetting.Audit_MailSMPT_PORT = Convert.ToInt32(wReader["AsidetSSMAIL_Port"]);
                         item.AuditMailSetting.Audit_MailRecipients = wReader["AsiMailRecipients"].ToString();
                         item.AuditMailSetting.Audit_MailSMTP_SERVER = wReader["AsidetSSMAIL_Server"].ToString();
                         item.AuditMailSetting.Audit_MailSender = wReader["AsiDetMSMailSender"].ToString();
                         item.AuditMailSetting.Audit_MailPassword = wReader["AsiDetMSPassword"].ToString();
                         item.AuditMailSetting.Audit_MailUserName = wReader["AsiDetMSUserName"].ToString();
                         //  item.AuditMailSetting.Audit_MailSendMail = Convert.ToBoolean(reader["AsiSendMail"]);
                         if (wReader["AsiDetMSEnableSSL"] != DBNull.Value)
                             item.AuditMailSetting.Audit_MailEnableSSL = Convert.ToBoolean(wReader["AsiDetMSEnableSSL"]);
                         if (wReader["AsiConnectionStringDest"] != DBNull.Value)
                             item.AsiConnectionStringDest = Convert.ToString(wReader["AsiConnectionStringDest"]);
                         if (wReader["DataBaseNameDest"] != DBNull.Value)
                             item.DataBaseNameDest = Convert.ToString(wReader["DataBaseNameDest"]);
                         if (wReader["DataBaseName"] != DBNull.Value)
                             item.DataBaseName = Convert.ToString(wReader["DataBaseName"]);
                         if (wReader["DataBaseID"] != DBNull.Value)
                             item.DataBaseID = Convert.ToInt32(wReader["DataBaseID"]);
                         if (wReader["AccountGroupUnique"] != DBNull.Value)
                             item.AccountGroupUnique = Guid.Parse(wReader["AccountGroupUnique"].ToString());
                         if (wReader["AccountGroupDescrip"] != DBNull.Value)
                             item.AccountGroupDescrip = wReader["AccountGroupDescrip"].ToString();
                         if (wReader["AsiProcessDetailsOnTarget"] != DBNull.Value)
                             item.AsiProcessDetailsOnTarget = Convert.ToBoolean(wReader["AsiProcessDetailsOnTarget"]);
                         if (wReader["OriginalTypeId"] != DBNull.Value)
                             item.OriginalTypeId = Convert.ToInt32(wReader["OriginalTypeId"]);
                         item.OriginalTypeName = wReader["OriginalTypeName"].ToString();

                         if ((int)item.ChannelType != (int)PelsoftEnums.Channeltype.AttentionQueue)
                             list.Add(item);
                     }
                 }

                 return list;
             },
             (ex) => throw ManageException(ex));
        }
    }
}
