namespace Pelsoft.Log.Common
{
    using System;
    using System.Data;
    using Pelsoft.Log.Common.Data;
    using System.Data.SqlClient;

    public class PublishDAC
    {
        private static void ManageException(Exception pException, string pCnnStr, string pMsg)
        {
            PelsoftException wPelsoftEx = PelsoftException.ProcessException(pException, typeof(ProcessDAC), pCnnStr, pCnnStr);
            wPelsoftEx.ErrorCode = 1300;
            throw wPelsoftEx;
        }

        /// <summary>
        /// Obtiene los datos a publicar.
        /// </summary>
        public static PublishBEList GetPublish(string pConnectionString, Guid pAccountDetailUnique, Int32 pMaxRetriesQuantity,
                                               Int32 pQuantityToPublish, Pelsoft.Log.Common.PelsoftEnums.Channeltype pServiceChannelType)
        {
            return DBManager.ExecuteStoreProcedure<PublishBEList>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    PublishBE wPublishBE = null;
                    PublishBEList wPublishBEList = new PublishBEList();

                    cmd.CommandText = "MC.Publication_g";

                    cmd.Parameters.AddWithValue("@AccountDetailUnique", pAccountDetailUnique);
                    cmd.Parameters.AddWithValue("@PChannelType", pServiceChannelType);
                    cmd.Parameters.AddWithValue("@MaxRetriesQuantity", pMaxRetriesQuantity);
                    cmd.Parameters.AddWithValue("@QuantityToPublish", pQuantityToPublish);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wPublishBE = new PublishBE();

                            wPublishBE.PublicationId = Convert.ToInt32(dr["PublicationId"]);
                            wPublishBE.PComment = Convert.ToString(dr["PComment"]);
                            wPublishBE.PublicationTo = Convert.ToString(dr["PublicationTo"]);
                            wPublishBE.AccountDetailUnique = Guid.Parse(dr["AccountDetailUnique"].ToString());

                            if (dr["PublicationDate"] != DBNull.Value)
                                wPublishBE.PublicationDate = Convert.ToDateTime(dr["PublicationDate"]);

                            wPublishBE.PChannelType = Convert.ToInt32(dr["PChannelType"]);

                            if (dr["PublicationErrorId"] != DBNull.Value)
                                wPublishBE.PublicationErrorId = Convert.ToInt32(dr["PublicationErrorId"]);

                            wPublishBE.PRetriesQuantity = Convert.ToInt32(dr["PRetriesQuantity"]);

                            if (dr["PModifiedDate"] != DBNull.Value)
                                wPublishBE.PModifiedDate = Convert.ToDateTime(dr["PModifiedDate"]);

                            if (dr["PModifiedByUserId"] != DBNull.Value)
                                wPublishBE.PModifiedByUserId = Convert.ToInt32(dr["PModifiedByUserId"]);

                            wPublishBE.PAwaitingUserConfirmation = Convert.ToBoolean(dr["PAwaitingUserConfirmation"]);

                            if (dr["ProcessDetailId"] != DBNull.Value)
                                wPublishBE.ProcessDetailId = Convert.ToInt32(dr["ProcessDetailId"]);

                            wPublishBE.PActiveFlag = Convert.ToBoolean(dr["PActiveFlag"]);

                            if (dr["PublicationCC"] != DBNull.Value)
                                wPublishBE.PublicationCC = Convert.ToString(dr["PublicationCC"]);

                            if (dr["CaseCommentGUID"] != DBNull.Value)
                                wPublishBE.CaseCommentGuid = new Guid(dr["CaseCommentGUID"].ToString());

                            wPublishBEList.Add(wPublishBE);
                        }
                    }

                    return wPublishBEList;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.Publication_g: "));
        }

        /// <summary>
        /// Obtiene los adjuntos a publicar.
        /// </summary>
        public static PublishAttachmentBEList GetPublishAttachment(string pConnectionString, Int32 pPublicationId)
        {
            return DBManager.ExecuteStoreProcedure<PublishAttachmentBEList>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    PublishAttachmentBE wPublishAttachmentBE = null;
                    PublishAttachmentBEList wPublishAttachmentBEList = new PublishAttachmentBEList();

                    cmd.CommandText = "MC.PublicationAttachment_g";

                    cmd.Parameters.AddWithValue("@PublicationId", pPublicationId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wPublishAttachmentBE = new PublishAttachmentBE();

                            if (dr["PAttachmentName"] != DBNull.Value)
                                wPublishAttachmentBE.PAttachmentName = Convert.ToString(dr["PAttachmentName"]);

                            if (dr["PAttachment"] != DBNull.Value)
                                wPublishAttachmentBE.PAttachment = (Byte[])(dr["PAttachment"]);

                            if (dr["MediaCategory"] != DBNull.Value)
                                wPublishAttachmentBE.MediaCategory = (String)(dr["MediaCategory"]);

                            wPublishAttachmentBEList.Add(wPublishAttachmentBE);
                        }
                    }

                    return wPublishAttachmentBEList;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.PublicationAttachment_g: "));
        }

        /// <summary>
        /// Obtiene los detalles a publicar.
        /// </summary>
        public static PublishDetailsBEList GetPublishDetail(string pConnectionString, Int32 pPublicationId)
        {
            return DBManager.ExecuteStoreProcedure<PublishDetailsBEList>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    PublishDetailsBE wPublishDetailsBE = null;
                    PublishDetailsBEList wPublishDetailsBEList = new PublishDetailsBEList();

                    cmd.CommandText = "MC.PublicationDetail_g";

                    cmd.Parameters.AddWithValue("@PublicationId", pPublicationId);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wPublishDetailsBE = new PublishDetailsBE();

                            wPublishDetailsBE.PDCommentAux1 = Convert.ToString(dr["PDCommentAux1"]);
                            wPublishDetailsBE.PDCommentAux5 = Convert.ToString(dr["PDCommentAux5"]);

                            if(dr["PDJsonPublication"] != DBNull.Value)
                                wPublishDetailsBE.PDJsonPublication = Convert.ToString(dr["PDJsonPublication"]);

                            wPublishDetailsBEList.Add(wPublishDetailsBE);
                        }
                    }

                    return wPublishDetailsBEList;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.PublicationDetail_g: "));
        }

        /// <summary>
        /// Modifica la tabla de Publicación.
        /// </summary>
        public static void UpdatePublish(string pConnectionString, Int32 pPublicationId, DateTime pPublicationDate, Int32 pProcessDetailId, Int32 pPModifiedByUserId, string pPSourcePublicationId = "")
        {
            DBManager.ExecuteStoreProcedure(
                pConnectionString,
                (cnn, cmd) =>
                {
                    cmd.CommandText = "MC.Publication_u_Publish";

                    cmd.Parameters.AddWithValue("@PublicationId", pPublicationId);
                    cmd.Parameters.AddWithValue("@PublicationDate", pPublicationDate);
                    cmd.Parameters.AddWithValue("@ProcessDetailId", pProcessDetailId);
                    cmd.Parameters.AddWithValue("@PModifiedByUserId", pPModifiedByUserId);
                    cmd.Parameters.AddWithValue("@PSourcePublicationId", pPSourcePublicationId);

                    cmd.ExecuteNonQuery();
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.Publication_u_Publish: "));
        }

        /// <summary>
        /// Obtiene la ApplicationSettings.
        /// </summary>
        public static ApplicationSettingsBE GetApplicationSettings(string pConnectionString, Int32 pSettingId)
        {
            return DBManager.ExecuteStoreProcedure<ApplicationSettingsBE>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    ApplicationSettingsBE wApplicationSettingsBE = null;

                    cmd.CommandText = "Service.ApplicationSettings_s_BySettingId";

                    cmd.Parameters.AddWithValue("@SettingId", pSettingId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wApplicationSettingsBE = new ApplicationSettingsBE();

                            wApplicationSettingsBE.SettingId = Convert.ToInt32(dr["SettingId"]);
                            wApplicationSettingsBE.Description = Convert.ToString(dr["Description"]);
                            wApplicationSettingsBE.Value = Convert.ToString(dr["Value"]);
                        }
                    }

                    return wApplicationSettingsBE;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener Service.ApplicationSettings_s_BySettingId: "));
        }

        /// <summary>
        /// Obtiene el id del error.
        /// </summary>
        public static PublishErrorBE GetPublishError(string pConnectionString, Pelsoft.Log.Common.PelsoftEnums.Channeltype pServiceChannelType, String pPublicationErrorName)
        {
            return DBManager.ExecuteStoreProcedure<PublishErrorBE>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    PublishErrorBE wPublishErrorBE = null;

                    cmd.CommandText = "MC.PublicationError_g";

                    cmd.Parameters.AddWithValue("@PChannelType", pServiceChannelType);
                    cmd.Parameters.AddWithValue("@PublicationErrorName", pPublicationErrorName.Trim());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wPublishErrorBE = new PublishErrorBE();

                            wPublishErrorBE.PublicationErrorId = Convert.ToInt32(dr["PublicationErrorId"]);
                            wPublishErrorBE.PEAlertedByService = Convert.ToBoolean(dr["PEAlertedByService"]);
                            wPublishErrorBE.PEContinueToNextContact = Convert.ToBoolean(dr["PEContinueToNextContact"]);
                            wPublishErrorBE.PEServiceCycleCancel = Convert.ToBoolean(dr["PEServiceCycleCancel"]);
                        }
                    }

                    return wPublishErrorBE;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.PublicationError_g: "));
        }

        /// <summary>
        /// Modifica la tabla de Publicación.
        /// </summary>
        public static void UpdatePublishError(string pConnectionString, Int32 pPublicationId, Int32 pPublicationErrorId, Int32 pPModifiedByUserId)
        {
            DBManager.ExecuteStoreProcedure(
                pConnectionString,
                (cnn, cmd) =>
                {
                    cmd.CommandText = "MC.Publication_u_Error";

                    cmd.Parameters.AddWithValue("@PublicationId", pPublicationId);
                    cmd.Parameters.AddWithValue("@PublicationErrorId", pPublicationErrorId);
                    cmd.Parameters.AddWithValue("@PModifiedByUserId", pPModifiedByUserId);

                    cmd.ExecuteNonQuery();
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.Publication_u_Error: "));
        }

        /// <summary>
        /// Modifica la tabla de Publicación.
        /// </summary>
        public static Int32 InsertPublishError(string pConnectionString, Pelsoft.Log.Common.PelsoftEnums.Channeltype pServiceChannelType, Exception pEx, string pWepException = "")
        {
            return DBManager.ExecuteStoreProcedure<Int32>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    string wErrorName = string.Empty;

                    if (!string.IsNullOrEmpty(pWepException))
                        wErrorName = pEx.Message + " " + pWepException;
                    else
                        wErrorName = pEx.Message;

                    cmd.CommandText = "MC.PublicationErrors_i";

                    cmd.Parameters.AddWithValue("@PChannelType", pServiceChannelType);
                    cmd.Parameters.AddWithValue("@PublicationErrorName", wErrorName);
                    cmd.Parameters.AddWithValue("@PublicationErrorDescription", pEx.StackTrace);

                    cmd.Parameters.Add("@PublicationErrorId", SqlDbType.Int, sizeof(int)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    return Convert.ToInt32(cmd.Parameters["@PublicationErrorId"].Value);
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.PublicationError_i: "));
        }

        /// <summary>
        /// Obtiene los datos a publicar para el mensajero.
        /// </summary>
        public static PublishBEList Mensajero_Publication(string pConnectionString, Guid pAccountDetailUnique, Int32 pRetriesQuantity, Int32 pQuantityToPublish)
        {
            return DBManager.ExecuteStoreProcedure<PublishBEList>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    PublishBE wPublishBE = null;
                    PublishBEList wPublishBEList = new PublishBEList();

                    cmd.CommandText = "[WS].[Mensajero_Publication]";
                    cmd.Parameters.AddWithValue("@AccountDetailUnique", pAccountDetailUnique);
                    cmd.Parameters.AddWithValue("@QuantityToPublish", pQuantityToPublish);
                    cmd.Parameters.AddWithValue("@MaxRetriesQuantity", pRetriesQuantity);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            wPublishBE = new PublishBE();

                            if (dr["PublicationId"] != DBNull.Value)
                                wPublishBE.PublicationId = Convert.ToInt32(dr["PublicationId"]);

                            if (dr["PComment"] != DBNull.Value)
                                wPublishBE.PComment = Convert.ToString(dr["PComment"]);

                            if (dr["PublicationTo"] != DBNull.Value)
                                wPublishBE.PublicationTo = Convert.ToString(dr["PublicationTo"]);

                            if (dr["AccountDetailUnique"] != DBNull.Value)
                                wPublishBE.AccountDetailUnique = Guid.Parse(dr["AccountDetailUnique"].ToString());

                            if (dr["PChannelType"] != DBNull.Value)
                                wPublishBE.PChannelType = Convert.ToInt32(dr["PChannelType"]);

                            wPublishBEList.Add(wPublishBE);
                        }
                    }

                    return wPublishBEList;
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener [WS].[Mensajero_Publication]: "));
        }

        /// <summary>
        /// Obtiene los datos de un caso buscando por CaseId.    
        /// </summary>
        public static bool Case_s_ByCaseId_Status(string pConnectionString, Int32 pCaseId)
        {
            return DBManager.ExecuteStoreProcedure<bool>(
                pConnectionString,
                (cnn, cmd) =>
                {
                    cmd.CommandText = "[WS].[Case_s_ByCaseId_Status]";

                    cmd.Parameters.AddWithValue("@CaseId", pCaseId);
                    cmd.Parameters.Add("@open", SqlDbType.Bit, sizeof(bool)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@open"].Value);
                },
                (ex) => ManageException(ex, pConnectionString, "No se puede obtener MC.Publication_g: "));
        }
    }
}
