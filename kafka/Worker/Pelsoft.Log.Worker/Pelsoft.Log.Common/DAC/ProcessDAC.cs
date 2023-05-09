namespace Pelsoft.Log.Common
{
    using System;
    using System.Data;
    using Pelsoft.Log.Common.Data;
    using System.Data.SqlClient;
    using Pelsoft.Log.Common.Workers;

    public class ProcessDAC
    {

        internal static AccountsInstancesServicesBE Get_AccountsInstanceByParams(Guid accountDetailUnique, Guid asiDetailUnique, Guid instancesServiceGuid, Guid AccountGroupUnique)
        {
            return DBManager.ExecuteStoreProcedure<AccountsInstancesServicesBE>(
            CommonWorkers.Instance.Cnnstring_Master,
            (cnn, cmd) =>
            {
                AccountsInstancesServicesBE item = null;

                cmd.CommandText = "Config.AccountServiceInstance_g_ByParams";

                cmd.Parameters.AddWithValue("@AccountDetailUnique", accountDetailUnique);
                cmd.Parameters.AddWithValue("@AsiDetailUnique", asiDetailUnique);
                cmd.Parameters.AddWithValue("@ServiceInstance", instancesServiceGuid);
                cmd.Parameters.AddWithValue("@AccountGroupUnique", AccountGroupUnique);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new AccountsInstancesServicesBE();

                        item.AsiDetailUnique = Guid.Parse(reader["AsiDetailUnique"].ToString());
                        if (reader["AccountDetailUnique"] != DBNull.Value)
                            item.AccountDetailUnique = Guid.Parse(reader["AccountDetailUnique"].ToString());
                        if (reader["AccountDetailDescrip"] != DBNull.Value)
                            item.AccountDetailDescrip = reader["AccountDetailDescrip"].ToString();
                        item.Interval = Convert.ToInt32(reader["AsiDetailInterval"]);
                        item.AccountName = reader["AccountName"].ToString();
                        item.ServiceChannelName = reader["ChannelName"].ToString();
                        item.ProcessDetailsOnTarget = Convert.ToBoolean(reader["AsiProcessDetailsOnTarget"]);
                        if (reader["ProxyHost"] != DBNull.Value)
                        {
                            item.Proxy.ProxyHost = reader["ProxyHost"].ToString();
                            item.Proxy.ProxyUserName = reader["ProxyUserName"].ToString();
                            item.Proxy.ProxyPassword = reader["ProxyPassword"].ToString();
                            item.Proxy.ProxyDomain = reader["ProxyDomain"].ToString();
                            item.Proxy.ProxyPort = Convert.ToInt32(reader["ProxyPort"]);
                        }
                        else
                        {
                            item.Proxy = null;
                        }
                        item.ConnectionString = reader["AsiConnectionString"].ToString();
                        item.ChannelType = (Pelsoft.Log.Common.PelsoftEnums.Channeltype)Convert.ToInt32(reader["ChannelType"]);
                        item.AuditMailSetting = new AuditMailSetting_Struct();
                        if (reader["AsidetSSMAIL_Port"] != DBNull.Value)
                            item.AuditMailSetting.Audit_MailSMPT_PORT = Convert.ToInt32(reader["AsidetSSMAIL_Port"]);
                        item.AuditMailSetting.Audit_MailRecipients = reader["AsiMailRecipients"].ToString();
                        item.AuditMailSetting.Audit_MailSMTP_SERVER = reader["AsidetSSMAIL_Server"].ToString();
                        item.AuditMailSetting.Audit_MailSender = reader["AsiDetMSMailSender"].ToString();
                        item.AuditMailSetting.Audit_MailPassword = reader["AsiDetMSPassword"].ToString();
                        item.AuditMailSetting.Audit_MailUserName = reader["AsiDetMSUserName"].ToString();
                        //item.AuditMailSetting.Audit_MailSendMail = Convert.ToBoolean(reader["AsiSendMail"]);
                        if (reader["AsiDetMSEnableSSL"] != DBNull.Value)
                            item.AuditMailSetting.Audit_MailEnableSSL = Convert.ToBoolean(reader["AsiDetMSEnableSSL"]);
                        if (reader["AsiConnectionStringDest"] != DBNull.Value)
                            item.AsiConnectionStringDest = Convert.ToString(reader["AsiConnectionStringDest"]);
                        if (reader["DataBaseNameDest"] != DBNull.Value)
                            item.DataBaseNameDest = Convert.ToString(reader["DataBaseNameDest"]);
                        if (reader["DataBaseName"] != DBNull.Value)
                            item.DataBaseName = Convert.ToString(reader["DataBaseName"]);
                        if (reader["DataBaseID"] != DBNull.Value)
                            item.DataBaseID = Convert.ToInt32(reader["DataBaseID"]);
                        if (reader["AccountGroupUnique"] != DBNull.Value)
                            item.AccountGroupUnique = Guid.Parse(reader["AccountGroupUnique"].ToString());
                        if (reader["AccountGroupDescrip"] != DBNull.Value)
                            item.AccountGroupDescrip = reader["AccountGroupDescrip"].ToString();
                        if (reader["AsiProcessDetailsOnTarget"] != DBNull.Value)
                            item.AsiProcessDetailsOnTarget = Convert.ToBoolean(reader["AsiProcessDetailsOnTarget"]);
                        if (reader["OriginalTypeId"] != DBNull.Value)
                            item.OriginalTypeId = Convert.ToInt32(reader["OriginalTypeId"]);
                        item.OriginalTypeName = reader["OriginalTypeName"].ToString();
                    }
                }

                return item;
            },
            (ex) => PelsoftException.ProcessException(ex, typeof(ProcessDAC), CommonWorkers.Instance.Cnnstring_Master));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceInstanceUnique"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static int CreateNewProcess(Guid serviceInstanceUnique,
                                        AccountsInstancesServicesBE account)
        {
            Guid? originalGUID = null;

            if (account.OriginalTypeId.Equals((int)PelsoftEnums.OriginalType.Account))
                originalGUID = account.AccountDetailUnique;
            else
                originalGUID = account.AccountGroupUnique;

            if (!account.ProcessDetailsOnTarget)
                return CreateNewProcess(serviceInstanceUnique, account.AsiDetailUnique, originalGUID.Value, account.ChannelType, account.ConnectionString, account.OriginalTypeId);
            else
                return CreateNewProcess(serviceInstanceUnique, account.AsiDetailUnique, originalGUID.Value, account.ChannelType, account.AsiConnectionStringDest, account.OriginalTypeId);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceInstanceUnique"></param>
        /// <param name="asiDetailUnique"></param>
        /// <param name="accountDetailUnique"></param>
        /// <param name="serviceChannelType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int CreateNewProcess(Guid serviceInstanceUnique,
                                           Guid asiDetailUnique,
                                           Guid originalGUID,
                                           Pelsoft.Log.Common.PelsoftEnums.Channeltype serviceChannelType,
                                           string connectionString, int pOriginalTypeId)
        {
            return DBManager.ExecuteStoreProcedure<int>(
            connectionString,
            (cnn, cmd) =>
            {
                cmd.CommandText = "Log.ProcessDetails_i";

                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ServiceInstanceUnique", serviceInstanceUnique);
                cmd.Parameters.AddWithValue("@AsiDetailUnique", asiDetailUnique);
                cmd.Parameters.AddWithValue("@OriginalGUID", originalGUID);
                cmd.Parameters.AddWithValue("@PServiceChannelType", (int)serviceChannelType);
                cmd.Parameters.AddWithValue("@POriginalTypeId", (int)pOriginalTypeId);

                cmd.Parameters.Add("@ProcessDetailsId", SqlDbType.Int, sizeof(int)).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@ProcessDetailsId"].Value);
            },
            (ex) => {
                PelsoftException ee = PelsoftException.ProcessException(ex, typeof(ProcessDAC), "No se puede crear ProcessDetails debido a:", connectionString);
                ee.ErrorCode = 1302;
                ee.ErrorCode = 1310; //fatal
                throw ee;
            });
        }

        /// <summary>
        /// Llama ProcessDetails_u_Closure chequeando ProcessDetailsOnTarget
        /// </summary>
        /// <param name="currentProcessDetailsID"></param>
        /// <param name="account"></param>
        public static void CloseProcessDetails(int currentProcessDetailsID, AccountsInstancesServicesBE account)
        {
            if (!account.ProcessDetailsOnTarget)
                CloseProcessDetails(currentProcessDetailsID, account.ConnectionString);
            else
                CloseProcessDetails(currentProcessDetailsID, account.AsiConnectionStringDest);
        }

        /// <summary>
        /// ProcessDetails_u_Closure
        /// </summary>
        /// <param name="currentProcessDetailsID"></param>
        /// <param name="cnnstring_logs"></param>
        public static void CloseProcessDetails(int currentProcessDetailsID, string connectionString)
        {
            DBManager.ExecuteStoreProcedure(
             connectionString,
             (cnn, cmd) =>
             {
                 cmd.CommandText = "Log.ProcessDetails_u_Closure";

                 cmd.Parameters.AddWithValue("@ProcessDetailsId", currentProcessDetailsID);

                 cmd.ExecuteNonQuery();
             },
             (ex) => {
                 PelsoftException ee = PelsoftException.ProcessException(ex, typeof(ProcessDAC), "No se puede cerrar ProcessDetails debido a:", connectionString);
                 ee.ErrorCode = 1303;
                 throw ee;
             });
        }

        ///// <summary>
        ///// Llama a ProcessSettings_ByGuid
        ///// </summary>
        ///// <param name="asiDetailUnique"></param>
        ///// <param name="serviceInstanceGuid"></param>
        ///// <param name="asiDetailUnique"></param>
        ///// <param name="connectionString"></param>
        ///// <returns></returns>
        //public static List<ProcessSettings> GetProcessSetting(Guid accountDetailUnique, Guid serviceInstanceGuid, Guid asiDetailUnique, string connectionString)
        //{
        //    return DBManager.ExecuteStoreProcedure<List<ProcessSettings>>(
        //     connectionString,
        //     (cnn, cmd) =>
        //     {
        //         ProcessSettings wProcessSetting = null;
        //         List<ProcessSettings> wProcessSettingList = new List<ProcessSettings>();

        //         cmd.CommandText = "Log.ProcessSettings_ByGuid";

        //         cmd.Parameters.AddWithValue("@AccountDetailUnique", accountDetailUnique);
        //         cmd.Parameters.AddWithValue("@ServiceInstanceUnique", serviceInstanceGuid);
        //         cmd.Parameters.AddWithValue("@AsiDetailUnique", asiDetailUnique);

        //         using (SqlDataReader dr = cmd.ExecuteReader())
        //         {
        //             while (dr.Read())
        //             {
        //                 wProcessSetting = new ProcessSetting();
        //                 wProcessSetting.ProcessSettingid = Convert.ToInt32(dr["ProcessSettingid"]);
        //                 wProcessSetting.PSQuantityMinutes = Convert.ToInt32(dr["PSQuantityMinutes"]);
        //                 if (dr["PSStartMinutes"] != DBNull.Value)
        //                     wProcessSetting.PSStartMinutes = Convert.ToInt32(dr["PSStartMinutes"]);
        //                 if (dr["PSStartTime"] != DBNull.Value)
        //                     wProcessSetting.PSStartTime = Convert.ToInt32(dr["PSStartTime"]);

        //                 wProcessSettingList.Add(wProcessSetting);
        //             }
        //         }

        //         return wProcessSettingList;
        //     },
        //     (ex) => {
        //         PelsoftException ee = PelsoftException.ProcessException(ex, typeof(ProcessDAC), "No se puede obtener ProcessSettings cnnString: ", connectionString);
        //         ee.ErrorCode = "1300";
        //         throw ee;
        //     });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceInstanceUnique"></param>
        /// <param name="asiDetailUnique"></param>
        /// <param name="accountDetailUnique"></param>
        /// <param name="serviceChannelType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int InsertServiceProcessDetails(Guid pAccountDetailUnique, Guid pServiceInstanceUnique,
                                                      Guid pAsiDetailUnique, Pelsoft.Log.Common.PelsoftEnums.Channeltype pServiceChannelType,
                                                      string connectionString)
        {
            return DBManager.ExecuteStoreProcedure<int>(
            connectionString,
            (cnn, cmd) =>
            {
                cmd.CommandText = "Service.ProcessDetails_i";

                cmd.Parameters.AddWithValue("@AccountDetailUnique", pAccountDetailUnique);
                cmd.Parameters.AddWithValue("@ServiceInstanceUnique", pServiceInstanceUnique);
                cmd.Parameters.AddWithValue("@AsiDetailUnique", pAsiDetailUnique);
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PServiceChannelType", (int)pServiceChannelType);
                cmd.Parameters.AddWithValue("@CicleExecute", true);

                cmd.Parameters.Add("@ProcessDetailsId", SqlDbType.Int, sizeof(int)).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@ProcessDetailsId"].Value);
            },
            (ex) => {
                PelsoftException ee = PelsoftException.ProcessException(ex, typeof(ProcessDAC), "No se puede crear Service.ProcessDetails debido a:", connectionString);
                ee.ErrorCode = 1302;
                throw ee;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessDetailsId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static void UpdateServiceProcessDetails(Int32 pProcessDetailsId, string connectionString)
        {
            DBManager.ExecuteStoreProcedure(
             connectionString,
             (cnn, cmd) =>
             {
                 cmd.CommandText = "Service.ProcessDetails_u_Closure";

                 cmd.Parameters.AddWithValue("@ProcessDetailsId", pProcessDetailsId);

                 cmd.ExecuteNonQuery();
             },
             (ex) => {
                 PelsoftException ee = PelsoftException.ProcessException(ex, typeof(ProcessDAC), "No se puede modificar Service.ProcessDetails debido a:", connectionString);
                 ee.ErrorCode = 1302;
                 throw ee;
             });
        }

        /// <summary>
        /// Inserta en la tabla Service.Status.
        /// </summary>
        /// <param name="pConnectionString"></param>
        public static void CreateStatus(string pConnectionString, Int32 pProcessDetailsId, string pStatusDescription, Int32 pPServiceChannelType, string pStatusResponse, string pStatusError)
        {
            DBManager.ExecuteStoreProcedure(
             pConnectionString,
             (cnn, cmd) =>
             {
                 cmd.CommandText = "Service.Status_i";

                 cmd.Parameters.AddWithValue("@ProcessDetailsId", pProcessDetailsId);
                 cmd.Parameters.AddWithValue("@StatusDescription", pStatusDescription);
                 cmd.Parameters.AddWithValue("@PServiceChannelType", pPServiceChannelType);
                 cmd.Parameters.AddWithValue("@StatusResponse", pStatusResponse);
                 cmd.Parameters.AddWithValue("@StatusError", pStatusError);

                 cmd.ExecuteNonQuery();
             },
             (ex) => throw PelsoftException.ProcessException(ex, typeof(ProcessDAC), pConnectionString));
        }
    }
}
