using System;
using Pelsoft.Log.Common;
using Pelsoft.Log.Common.Data;
using Pelsoft.Log.WhatsApp.BE;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.WhatsApp.DAC
{
    public class WhatsAppDAC
    {
        #region WhatsAppUserBE

        /// <summary>
        /// Consulta el usuario por el campo WUserPhone
        /// </summary>
        /// <param name="whatsAppUser"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static WhatsAppUserBE GetUser(WhatsAppUserBE whatsAppUser, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   cmd.CommandText = "[Whatsapp].[User_g]";
                   cmd.Parameters.AddWithValue("@WUserPhone", whatsAppUser.WUserPhone);

                   using (SqlDataReader reader = cmd.ExecuteReader())
                   {
                       while (reader.Read())
                       {
                           if (reader["WUserName"] != DBNull.Value)
                               whatsAppUser.WUserName = reader["WUserName"].ToString();
                           if (reader["WUserImage"] != DBNull.Value)
                               whatsAppUser.WUserImage = (byte[])reader["WUserImage"];
                           if (reader["WUserModifiedDate"] != DBNull.Value)
                               whatsAppUser.WUserModifiedDate = (DateTime)reader["WUserModifiedDate"];
                           if (reader["WUserCreatedDate"] != DBNull.Value)
                               whatsAppUser.WUserCreatedDate = (DateTime)reader["WUserCreatedDate"];
                           whatsAppUser.WUserId = (int)reader["WUserId"];
                           whatsAppUser.WuserActiveFlag = (bool)reader["WuserActiveFlag"];
                       }
                   }

                   return whatsAppUser;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        /// <summary>
        /// Guarda el usuario con los parametros pasados
        /// </summary>
        /// <param name="whatsAppUser"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static WhatsAppUserBE InsertUser(WhatsAppUserBE whatsAppUser, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   cmd.CommandText = "[WhatsApp].[User_i]";

                   cmd.Parameters.AddWithValue("@WUserPhone", whatsAppUser.WUserPhone.Trim());
                   cmd.Parameters.AddWithValue("@WUserName", whatsAppUser.WUserName.Trim());
                   cmd.Parameters.AddWithValue("@WUserImage", whatsAppUser.WUserImage);

                   cmd.Parameters.AddWithValue("@WUserId", SqlDbType.Int).Direction = ParameterDirection.Output;

                   cmd.ExecuteNonQuery();
                   whatsAppUser.WUserId = Convert.ToInt32(cmd.Parameters["@WUserId"].Value);

                   return whatsAppUser;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        /// <summary>
        /// Actualiza el nombre o imagen del usuario
        /// </summary>
        /// <param name="whatsAppUser"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool UpdateUser(WhatsAppUserBE whatsAppUser, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   cmd.CommandText = "[Whatsapp].[User_u]";

                   cmd.Parameters.AddWithValue("@WUserId", whatsAppUser.WUserId);
                   cmd.Parameters.AddWithValue("@WUserName", whatsAppUser.WUserName);
                   cmd.Parameters.AddWithValue("@WUserImage", whatsAppUser.WUserImage);

                   return cmd.ExecuteNonQuery() > 0;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        #endregion

        #region WhatsAppMessageBE

        /// <summary>
        /// Guarda el mensaje de WhatsApp
        /// </summary>
        /// <param name="whatsAppMessageBE"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static WhatsAppMessageBE InsertWhatsAppMessage(WhatsAppMessageBE whatsAppMessageBE, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
              connectionString,
              (cnn, cmd) =>
              {
                  cmd.CommandText = "[Whatsapp].[Message_i]";

                  cmd.Parameters.AddWithValue("@WMId", SqlDbType.Int).Direction = ParameterDirection.Output;

                  cmd.Parameters.AddWithValue("@WFromUserId", whatsAppMessageBE.WFromUserId);
                  cmd.Parameters.AddWithValue("@WCId", whatsAppMessageBE.WCId);
                  cmd.Parameters.AddWithValue("@ProcessDetailsId", whatsAppMessageBE.ProcessDetailsId);
                  cmd.Parameters.AddWithValue("@WMMessage", whatsAppMessageBE.WMMessage);
                  cmd.Parameters.AddWithValue("@WMExternalId", whatsAppMessageBE.WMExternalId);
                  cmd.Parameters.AddWithValue("@WMState", whatsAppMessageBE.WMState);
                  cmd.Parameters.AddWithValue("@WMType", whatsAppMessageBE.WMType);
                  cmd.Parameters.AddWithValue("@WMDeliveredDate", whatsAppMessageBE.WMDeliveredDate);
                  cmd.Parameters.AddWithValue("@WMReadDate", whatsAppMessageBE.WMReadDate);
                  cmd.Parameters.AddWithValue("@WMCreatedDate", whatsAppMessageBE.WMCreatedDate);
                  cmd.Parameters.AddWithValue("@AccountDetailUnique", whatsAppMessageBE.AccountDetailUnique);

                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["@WMId"].SqlValue.ToString() != "Null")
                      whatsAppMessageBE.WMId = int.Parse(cmd.Parameters["@WMId"].SqlValue.ToString());

                  return whatsAppMessageBE;
              },
              (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        #endregion

        #region WhatsAppMessageMediaBE

        /// <summary>
        /// Guarda el adjunto del mensaje de WhatsApp
        /// </summary>
        /// <param name="whatsAppMessageMediaBE"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool InsertWhatsAppMessageMedia(WhatsAppMessageMediaBE whatsAppMessageMediaBE, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   cmd.CommandText = "[Whatsapp].[MessageMedia_i]";

                   cmd.Parameters.AddWithValue("@WMId", whatsAppMessageMediaBE.WMId);
                   cmd.Parameters.AddWithValue("@WMMediaType", whatsAppMessageMediaBE.WMMediaType);
                   cmd.Parameters.AddWithValue("@WMFileId", whatsAppMessageMediaBE.WMFileId);
                   cmd.Parameters.AddWithValue("@WMFileInBytes", whatsAppMessageMediaBE.WMFileInBytes);
                   cmd.Parameters.AddWithValue("@WMMimeType", whatsAppMessageMediaBE.WMMimeType);
                   cmd.Parameters.AddWithValue("@MediaTypeId", whatsAppMessageMediaBE.MediaTypeId);
                   cmd.Parameters.AddWithValue("@WMName", whatsAppMessageMediaBE.WMName);
                   cmd.Parameters.AddWithValue("@WMOriginalAttachment", whatsAppMessageMediaBE.WMOriginalAttachment);

                   return cmd.ExecuteNonQuery() > 0;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        #endregion

        #region WhatsAppMessageBotBE

        /// <summary>
        /// Consulta los mensaje recibidos del Hook de WhatsApp
        /// </summary>
        /// <param name="whatsAppConfigBE"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static WhatsAppMessageBotBEList GetMessageBot(WhatsAppConfigBE whatsAppConfigBE, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   WhatsAppMessageBotBEList wWhatsAppMessageBotBEList = new WhatsAppMessageBotBEList();

                   cmd.CommandText = "[WhatsApp].[MessageBot_g]";

                   cmd.Parameters.AddWithValue("@WWhatsAppId", whatsAppConfigBE.WAccountPhone);
                   cmd.Parameters.AddWithValue("@MessageQuantityLog", whatsAppConfigBE.WCMessageQuantityLog);
                   cmd.Parameters.AddWithValue("@RetriesQuantityLog", whatsAppConfigBE.WCRetriesQuantityLog);

                   using (SqlDataReader reader = cmd.ExecuteReader())
                   {
                       while (reader.Read())
                       {
                           WhatsAppMessageBotBE wWhatsAppMessageBotBE = new WhatsAppMessageBotBE();

                           if (reader["WMessageBotMessage"] != DBNull.Value)
                               wWhatsAppMessageBotBE.WMessageBotMessage = reader["WMessageBotMessage"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotAttachment = reader["WMessageBotAttachment"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotMediaType = reader["WMessageBotMediaType"].ToString();
                           if (reader["WMessageBotProcessDate"] != DBNull.Value)
                               wWhatsAppMessageBotBE.WMessageBotProcessDate = (DateTime)reader["WMessageBotProcessDate"];
                           wWhatsAppMessageBotBE.WProviderId = reader["WProviderId"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotParam = reader["WMessageBotParam"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotTemplate = reader["WMessageBotTemplate"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotNameSpace = reader["WMessageBotNameSpace"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotDeliveredDateEpoch = (long)reader["WMessageBotDeliveredDate"];
                           if (reader["WMessageBotReadDate"] != DBNull.Value)
                               wWhatsAppMessageBotBE.WMessageBotReadDateEpoch = (long)reader["WMessageBotReadDate"];
                           wWhatsAppMessageBotBE.WMessageBotCreatedDateEpoch = (long)reader["WMessageBotCreatedDate"];
                           wWhatsAppMessageBotBE.WMessageBotId = (int)reader["WMessageBotId"];
                           wWhatsAppMessageBotBE.WMessageBotPhone = reader["WMessageBotPhone"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotExternalId = reader["WMessageBotExternalId"].ToString();
                           wWhatsAppMessageBotBE.WWhatsAppId = reader["WWhatsAppId"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotCreatedRow = (DateTime)reader["WMessageBotCreatedRow"];
                           wWhatsAppMessageBotBE.WMessageBotOrigin = reader["WMessageBotOrigin"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotStatus = reader["WMessageBotStatus"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotType = reader["WMessageBotType"].ToString();
                           wWhatsAppMessageBotBE.WMessageBotRedirected = (bool)reader["WMessageBotRedirected"];
                           if (reader["WMessageBotFileName"] != DBNull.Value)
                               wWhatsAppMessageBotBE.WMessageBotFileName = reader["WMessageBotFileName"].ToString();

                           wWhatsAppMessageBotBEList.Add(wWhatsAppMessageBotBE);
                       }
                   }

                   return wWhatsAppMessageBotBEList;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        /// <summary>
        /// Actualiza el mensaje leido de la tabla WhatsAppMessageBot
        /// </summary>
        /// <param name="whatsAppMessageBot"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool UpdateWhatsAppMessageBot(WhatsAppMessageBotBE whatsAppMessageBot, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
               connectionString,
               (cnn, cmd) =>
               {
                   cmd.CommandText = "[Whatsapp].[MessageBot_u]";

                   cmd.Parameters.AddWithValue("@WMessageBotId", whatsAppMessageBot.WMessageBotId);

                   return cmd.ExecuteNonQuery() > 0;
               },
               (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        /// <summary>
        /// Actualiza el mensaje leido de la tabla WhatsAppMessageBot para aumentar el numero de intentos fallidos
        /// </summary>
        /// <param name="whatsAppMessageBot"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool UpdateWhatsAppMessageBotRetriesQuantity(WhatsAppMessageBotBE whatsAppMessageBot, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure(
              connectionString,
              (cnn, cmd) =>
              {
                  cmd.CommandText = "[Whatsapp].[MessageBot_u_RetriesQuantity]";

                  cmd.Parameters.AddWithValue("@WMessageBotId", whatsAppMessageBot.WMessageBotId);

                  return cmd.ExecuteNonQuery() > 0;
              },
              (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        #endregion

        #region WhatsAppConfigBE

        /// <summary>
        /// Consulta la configuracion de WhatsApp
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public static WhatsAppConfigBE GetConfigWhatsApp(AccountsInstancesServicesBE accountInfo)
        {
            return DBManager.ExecuteStoreProcedure<WhatsAppConfigBE>(
              accountInfo.ConnectionString,
              (cnn, cmd) =>
              {
                  WhatsAppConfigBE wWhatsAppConfigBE = null;

                  cmd.CommandText = "[WhatsApp].[Config_g]";

                  cmd.Parameters.AddWithValue("@AccountDetailUnique", accountInfo.AccountDetailUnique);

                  using (SqlDataReader reader = cmd.ExecuteReader())
                  {
                      if (reader.Read())
                      {
                          wWhatsAppConfigBE = new WhatsAppConfigBE();
                          if (reader["WAccountId"] != DBNull.Value)
                              wWhatsAppConfigBE.WAccountId = (int)reader["WAccountId"];
                          if (reader["WCEndDate"] != DBNull.Value)
                              wWhatsAppConfigBE.WCEndDate = (DateTime)reader["WCEndDate"];
                          wWhatsAppConfigBE.WCActiveFlag = (bool)reader["WCActiveFlag"];
                          wWhatsAppConfigBE.WCCreated = (DateTime)reader["WCCreated"];
                          wWhatsAppConfigBE.WCId = (int)reader["WCId"];
                          wWhatsAppConfigBE.WCLimit = (int)reader["WCLimit"];
                          wWhatsAppConfigBE.WCPassword = reader["WCPassword"].ToString();
                          wWhatsAppConfigBE.WCQuantityAssignAttentionQueue = (int)reader["WCQuantityAssignAttentionQueue"];
                          wWhatsAppConfigBE.WCQuantityConvertToCase = (int)reader["WCQuantityConvertToCase"];
                          wWhatsAppConfigBE.WCQuantityToPublish = (int)reader["WCQuantityToPublish"];
                          wWhatsAppConfigBE.WCQuantityToPublishOnDemand = (int)reader["WCQuantityToPublishOnDemand"];
                          wWhatsAppConfigBE.WCRetriesQuantity = (int)reader["WCRetriesQuantity"];
                          wWhatsAppConfigBE.WCStartDate = (DateTime)reader["WCStartDate"];
                          wWhatsAppConfigBE.WCURLApiPublicacion = reader["WCURLApiPublicacion"].ToString();
                          wWhatsAppConfigBE.WCUserName = reader["WCUserName"].ToString();
                          wWhatsAppConfigBE.WAccountPhone = reader["WAccountPhone"].ToString();
                          wWhatsAppConfigBE.AccountDetailUnique = Guid.Parse(reader["AccountDetailUnique"].ToString());
                          wWhatsAppConfigBE.WCMessageQuantityLog = (int)reader["WCMessageQuantityLog"];
                          wWhatsAppConfigBE.WCRetriesQuantityLog = (int)reader["WCRetriesQuantityLog"];
                          if (reader["WCDestAccount"] != DBNull.Value)
                              wWhatsAppConfigBE.WCDestAccount = Convert.ToString(reader["WCDestAccount"]);
                          wWhatsAppConfigBE.WCShortenUrl = Convert.ToBoolean(reader["AccountDetailShortenUrl"]);
                      }
                  }

                  return wWhatsAppConfigBE;
              },
              (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), accountInfo.ConnectionString));
        }

        #endregion

        #region ErrorResponseByInternalCode

        public static string GetErrorReponseByInternalCode(int internalCode, string connectionString)
        {
            return DBManager.ExecuteStoreProcedure<string>(
             connectionString,
             (cnn, cmd) =>
             {
                 string descriptionInternalCode = string.Empty;

                 cmd.CommandText = "[Service].[ErrorResponse_s_ByInternalCode]";

                 cmd.Parameters.AddWithValue("@ErrorResponseInternalCode", internalCode);

                 using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                     if (reader.Read())
                         descriptionInternalCode = reader["ErrorResponseDescrip"].ToString();
                 }

                 return descriptionInternalCode;
             },
             (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), connectionString));
        }

        #endregion

        public static WhatsAppTemplateAttributesBEList GetTemplatesAttributesValuesByCaseCommentId(string pConectionString, Guid pCaseCommentGUID)
        {
            return DBManager.ExecuteStoreProcedure(
             pConectionString,
             (cnn, cmd) =>
             {
                 WhatsAppTemplateAttributesBEList wList = new WhatsAppTemplateAttributesBEList();
                 WhatsAppTemplateAttributesBE wItem;

                 cmd.CommandText = "[Service].[Whatsapp_TemplateAttributeValues_s_ByCaseCommentGUID]";

                 cmd.Parameters.AddWithValue("@CaseCommentGUID", pCaseCommentGUID);
                 using (SqlDataReader wRead = cmd.ExecuteReader())
                 {
                     while (wRead.Read())
                     {
                         wItem = new WhatsAppTemplateAttributesBE();
                         wItem.TemplateWhatsappId = wRead["TemplateWhatsappId"].ToString();
                         wItem.TemplateWhatsappName = wRead["TemplateWhatsappName"].ToString();
                         wItem.TAVValue = wRead["TAVValue"].ToString();
                         wItem.TAOrder = Convert.ToInt32(wRead["TAOrder"]);
                         wItem.TAVId = Convert.ToInt32(wRead["TAVId"]);
                         if (wRead["TemplateWithoutAttribute"] != DBNull.Value)
                             wItem.TemplateWithoutAttribute = Convert.ToBoolean(wRead["TemplateWithoutAttribute"]);

                         wList.Add(wItem);
                     }
                 }

                 return wList;
             },
             (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), pConectionString));
        }

        public static void UpdatePublicationDetail(string pConectionString, int pPublicationId, string pJSON)
        {
            DBManager.ExecuteStoreProcedure(
             pConectionString,
             (cnn, cmd) =>
             {
                 cmd.CommandText = "[MC].[PublicationDetail_u]";

                 cmd.Parameters.AddWithValue("@PublicationId", pPublicationId);
                 cmd.Parameters.AddWithValue("@PDJsonPublication", pJSON);

                 cmd.ExecuteNonQuery();
             },
             (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), pConectionString));
        }

        public static WhatsAppProviderConfigBE ProviderConfig_s(string pConectionString, string pProviderId, int pWCId)
        {
            return DBManager.ExecuteStoreProcedure<WhatsAppProviderConfigBE>(
             pConectionString,
             (cnn, cmd) =>
             {
                 WhatsAppProviderConfigBE wWhatsAppProviderConfig = null;

                 cmd.CommandText = "[Service].[ProviderConfig_s]";

                 cmd.Parameters.AddWithValue("@PCProviderId", pProviderId);
                 cmd.Parameters.AddWithValue("@PCWCId", pWCId);

                 using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                         wWhatsAppProviderConfig = new WhatsAppProviderConfigBE();
                         wWhatsAppProviderConfig.PCProviderId = pProviderId;
                         wWhatsAppProviderConfig.PCWCId = pWCId;
                         wWhatsAppProviderConfig.PCId = Convert.ToInt32(reader["PCId"]);
                         wWhatsAppProviderConfig.PCMessageBotReadDateUTC = Convert.ToBoolean(reader["PCMessageBotReadDateUTC"]);
                         wWhatsAppProviderConfig.PCMessageBotCreatedDateUTC = Convert.ToBoolean(reader["PCMessageBotCreatedDateUTC"]);
                         wWhatsAppProviderConfig.PCMessageBotDeliveredDateUTC = Convert.ToBoolean(reader["PCMessageBotDeliveredDateUTC"]);
                     }
                 }

                 return wWhatsAppProviderConfig;
             },
             (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsAppDAC), pConectionString));
        }
    }
}
