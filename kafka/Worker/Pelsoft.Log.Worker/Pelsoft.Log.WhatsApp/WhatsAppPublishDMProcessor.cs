using Pelsoft.Log.Common;
using Pelsoft.Log.Common.ProcessBases;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.WhatsApp.BE;
using Pelsoft.Log.WhatsApp.DAC;
using Pelsoft.Log.WhatsApp.wapi;
using konectaAPI.wapi;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp
{
    /// <summary>
    /// 
    /// </summary>
    public class WhatsAppPublishDMProcessor : ProcessorBase 
    {
        private WhatsAppConfigBE _WhatsAppConfig;
        private ApplicationSettingsBE _ApplicationSettings;
        private WAPIConnectorConfig _WAPIConfig = null;
        private PublishDetailsBEList _PublishDetails;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logService"></param>
        /// <param name="configuration"></param>
        /// <param name="accountInfo"></param>
        public WhatsAppPublishDMProcessor(IPelsoftLogService logService, IConfiguration configuration, AccountsInstancesServicesBE accountInfo) : base(logService, accountInfo)
        {

        }

        //public override void Start()
        //{
        //    base.Start();
        //}


        public override async Task DoWork(string jsonMessage,string topic)
        {
            var message = TrySerialize<KafkaWhatsAppMessage>(jsonMessage);

            await Log();
            
        }

        /// <summary>
        /// Verifica cambios en config y proxy
        /// </summary>
        public override void RefreshProcessData()
        {
            //Busca la configuración.
            this._WhatsAppConfig = WhatsAppDAC.GetConfigWhatsApp(this.AccountInfo);

            //Si no encuentra configuración se cancela.
            if (this._WhatsAppConfig == null)
                Helper.GeneratedException("No se encuentra la configuración para WhatsApp- |Verifique SP: WhatsApp.Config_g -> AccountDetailUnique| Clase: " + this.GetType().FullName + "  Metodo: RefreshProcessData | Posible Solucion: Verificar tabla -Facebook.Config- que este activeflag = 1 y que sea correcto el AccountDetailUnique || Valor actual de AccountDetailUnique : " + AccountInfo.AccountDetailUnique + "|||||", this.GetType(), this.SourceName);

            //Busca la ApplicattionSetting para obtener el usuario que publica
            _ApplicationSettings = PublishDAC.GetApplicationSettings(AccountInfo.AsiConnectionStringDest, 1);
            //Si no encuentra seteo se cancela.
            if (_ApplicationSettings == null)
                Helper.GeneratedException("No se encuentra ApplicationSettings para WhatsApp", this.GetType(), this.SourceName);

            this._WAPIConfig = this.ConfigConnection(this._WhatsAppConfig);
        }

        /// <summary>
        /// Configuración para wapi
        /// </summary>
        /// <param name="pWhatsAppConfig"></param>
        /// <returns></returns>
        private WAPIConnectorConfig ConfigConnection(WhatsAppConfigBE pWhatsAppConfig)
        {
            WAPIConnectorConfig wConfig = new WAPIConnectorConfig()
            {
                User = pWhatsAppConfig.WCUserName,
                Password = pWhatsAppConfig.WCPassword,
                Url = pWhatsAppConfig.WCURLApiPublicacion
            };

            if (this.Proxy != null)
            {
                wConfig.ProxyEnabled = true;
                wConfig.ProxyPort = this.AccountInfo.Proxy.ProxyPort.ToString();
                wConfig.ProxyName = this.AccountInfo.Proxy.ProxyHost;
                wConfig.ProxyPassword = this.AccountInfo.Proxy.ProxyPassword;
                wConfig.ProxyUser = this.AccountInfo.Proxy.ProxyUserName;
                wConfig.ProxyDomain = this.AccountInfo.Proxy.ProxyDomain;
            }

            return wConfig;
        }

        #region << -Manejo de Plantillas- >>
        /// <summary>
        /// Verifica si es un mensaje de tipo template. Utilizado generalmente para en envio primario de un mensaje. El rep al cliente.
        /// </summary>
        /// <param name="pStringConnection"></param>
        /// <param name="pPublicationId"></param>
        /// <returns></returns>
        private bool IsATemplateMessage(string pStringConnection, int pPublicationId)
        {
            //debe tener como maximo 1
            _PublishDetails = PublishDAC.GetPublishDetail(pStringConnection, pPublicationId);

            if (_PublishDetails.Count > 0)
            {
                string wAux5 = _PublishDetails.First().PDCommentAux5.Trim().ToUpper();
                if (wAux5 == "TEMPLATE")
                    return true;
                else
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Completa una plantilla con los datos guardados
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pConnectionString"></param>
        /// <param name="pCaseCommentGuid"></param>
        private void FillTemplateMessage(messageHSM pMessage, string pConnectionString, Guid pCaseCommentGuid)
        {
            WhatsAppTemplateAttributesBEList wTemplateAttributes = null;
            int wCount = 0;

            wTemplateAttributes = WhatsAppDAC.GetTemplatesAttributesValuesByCaseCommentId(pConnectionString, pCaseCommentGuid);
            if (wTemplateAttributes == null || wTemplateAttributes.Count == 0)
                throw new Exception("Error al cargar plantillas, No se encuentra bien configurada la misma. Dicho comentario no se corresponde con ninguna de las configuradas");

            pMessage.template = wTemplateAttributes.First().TemplateWhatsappId;
            pMessage.nameSpace = wTemplateAttributes.First().TemplateWhatsappName;
            if (!wTemplateAttributes.First().TemplateWithoutAttribute)
            {
                pMessage.@params = new String[wTemplateAttributes.Count];
                foreach (var wAttribute in wTemplateAttributes.OrderBy(T => T.TAOrder))
                {
                    wAttribute.TAVValue = wAttribute.TAVValue.Replace("\t", " ");
                    wAttribute.TAVValue = wAttribute.TAVValue.Replace("\r", " ");
                    wAttribute.TAVValue = wAttribute.TAVValue.Replace("\n", " ");
                    pMessage.@params[wCount] = wAttribute.TAVValue;
                    wCount++;
                }
            }
        }
        #endregion

        /// <summary>
        /// Metodo  se encarga de realizar la publicacion a traves del canal WhatsApp
        /// </summary>
        public async Task Log()
        {
            string wAttachmentBase64 = string.Empty;
            string wAttachmentName = string.Empty;
            string wAttachmentType = string.Empty;
            string wPublicationTo = string.Empty;
            Int32 wMCQuantityToPublish = 0;
            PublishAttachmentBEList wPublishAttachmentBEList = null;
            PublishBEList wPublishBEList = null;

            string _Response = string.Empty;
            string _Error = string.Empty;
            string _Status = string.Concat("Inicio proceso WS publicación privados: " + DateTime.Now.ToString("HH:mm"));

            if (PelsoftEnums.Channeltype.WhatsAppDMPublish == AccountInfo.ChannelType)
                wMCQuantityToPublish = this._WhatsAppConfig.WCQuantityToPublish;
            else
                wMCQuantityToPublish = this._WhatsAppConfig.WCQuantityToPublishOnDemand;

            _Status = string.Concat(_Status, ";Busca elementos a publicar: " + DateTime.Now.ToString("HH:mm"));

            //Buscamos los Mensajes a Publicar.
            //TODO: migra buscar por PublishId () Ver este tema en mmeet de como llegan los msg de kafka
            wPublishBEList = PublishDAC.GetPublish(AccountInfo.AsiConnectionStringDest,
                                                    this._WhatsAppConfig.AccountDetailUnique,
                                                    this._WhatsAppConfig.WCRetriesQuantity,
                                                    wMCQuantityToPublish,
                                                    AccountInfo.ChannelType);

            _Status = string.Concat(_Status, ";Comienza a publicar: " + wPublishBEList.Count().ToString() + " elementos. " + DateTime.Now.ToString("HH:mm"));

            //Task result;
            //Se procesa la lista de Mesajes a publicar
            foreach (PublishBE wPublishBE in wPublishBEList)
            {
                //Dio error y las cuentas son iguales seguimos con el proximo elemento.
                if (wPublicationTo != string.Empty && wPublicationTo == wPublishBE.PublicationTo)
                    continue;

                //Buscamos los adjuntos.
                wPublishAttachmentBEList = PublishDAC.GetPublishAttachment(AccountInfo.AsiConnectionStringDest, wPublishBE.PublicationId);

                //Convertimos el Arreglo de Binarios a base64
                if (wPublishAttachmentBEList != null && wPublishAttachmentBEList.Count > 0)
                {
                    wAttachmentName = wPublishAttachmentBEList[0].PAttachmentName.ToLower();
                    wAttachmentBase64 = Convert.ToBase64String(wPublishAttachmentBEList[0].PAttachment);

                    //Analizamos el contenido de los adjuntos a enviar
                    //Si es IMAGEN
                    if (wAttachmentName.Contains(".jpg") || wAttachmentName.Contains(".jpeg") || wAttachmentName.Contains(".png"))
                    {
                        if (wAttachmentName.Contains(".jpg"))
                            wAttachmentType = "jpg";
                        if (wAttachmentName.Contains(".jpeg"))
                            wAttachmentType = "jpeg";
                        if (wAttachmentName.Contains(".png"))
                            wAttachmentType = "png";
                    }
                    //Si es Documento
                    if (wAttachmentName.Contains(".pdf") || wAttachmentName.Contains(".doc") || wAttachmentName.Contains(".ppt") || wAttachmentName.Contains(".xls") || wAttachmentName.Contains(".txt"))
                    {
                        //PDF
                        if (wAttachmentName.Contains(".pdf"))
                            wAttachmentType = "pdf";
                        //DOC o DOCX
                        if (wAttachmentName.Contains(".doc"))
                            wAttachmentType = "doc";
                        //PPT o PPTX
                        if (wAttachmentName.Contains(".ppt"))
                            wAttachmentType = "ppt";
                        //XLS o XLSX
                        if (wAttachmentName.Contains(".xls"))
                            wAttachmentType = "xls";
                        //TXT
                        if (wAttachmentName.Contains(".txt"))
                            wAttachmentType = "txt";
                    }

                    //Analizamos si el adjunto es un AUDIO
                    if (wAttachmentName.Contains(".mp3") || wAttachmentName.Contains(".aac") || wAttachmentName.Contains(".amr") || wAttachmentName.Contains(".ogg") || wAttachmentName.Contains(".wav"))
                    {
                        if (wAttachmentName.Contains(".mp3"))
                            wAttachmentType = "mp3";
                        if (wAttachmentName.Contains(".aac"))
                            wAttachmentType = "aac";
                        if (wAttachmentName.Contains(".amr"))
                            wAttachmentType = "amr";
                        if (wAttachmentName.Contains(".ogg"))
                            wAttachmentType = "ogg";
                        if (wAttachmentName.Contains(".wav"))
                            wAttachmentType = "wav";
                    }

                    //Analizamos si el adjunto es un video
                    if (wAttachmentName.Contains(".mp4"))
                        wAttachmentType = "mp4";

                    //Analizamos si el adjunto es una carpeta comprimida.
                    if (wAttachmentName.Contains(".zip"))
                        wAttachmentType = "zip";

                    wPublishAttachmentBEList[0].PAttachmentMediaType = wAttachmentType.ToUpper();
                    this.SendDirectMessageWithAttachment(wPublishBE, wPublishAttachmentBEList[0], wAttachmentBase64);
                }
                else
                {
                    if (!this.IsATemplateMessage(AccountInfo.AsiConnectionStringDest, wPublishBE.PublicationId))
                        this.SendDirectMessage(wPublishBE);//Publicamos Mensaje de Texto
                    else
                        this.SendDirectMessageWithTemplate(wPublishBE);
                }
            }

            _Status = string.Concat(_Status, ";Fin WhatsApp publicación privados: " + DateTime.Now.ToString("HH:mm"));

            //Inserto el status.
            ProcessDAC.CreateStatus(AccountInfo.ConnectionString, this.currentProcessDetailsID, _Status, Convert.ToInt32(AccountInfo.ChannelType), _Response, _Error);
        }

        #region << -Envio de mansajes- >>
        /// <summary>
        /// Metodo para enviar mensajes de texto
        /// </summary>
        /// <param name="pPublish"></param>
        public void SendDirectMessage(PublishBE pPublish)
        {
            StringBuilder wStrBld = new StringBuilder();
            string wHtml = string.Empty, wJson = string.Empty, wContent = string.Empty;
            WhatsAppStatusBE wStatusResponse = new WhatsAppStatusBE();
            HttpResponseMessage wResultLog = null;
            messageHSM wMsg = null;
            WhatsAppResultBE wWhatsAppResult = null;
            Task<string> wTask = null;

            try
            {
                wMsg = new messageHSM();
                if (!string.IsNullOrEmpty(pPublish.PublicationTo))
                    wMsg.phones = new String[] { pPublish.PublicationTo };
                //se usa en caso que sea messagetype="TEMPLATE". 
                wMsg.messagetype = "TEXT";
                //Me fijo si se trata de encuestas. Si es así debo minimificar las urls en los mensajes, que al ser encuesta
                //debería tener no más de una url.
                if (this._WhatsAppConfig.WCShortenUrl && AccountInfo.ChannelType == PelsoftEnums.Channeltype.WhatsAppOnDemand)
                {
                    wMsg.text = Helper.MinifyTextUrls(pPublish.PComment);
                    wMsg.@params = new String[] { wMsg.text };
                }
                else
                {
                    wMsg.@params = new String[] { pPublish.PComment };
                    wMsg.text = pPublish.PComment;
                }

                wResultLog = WAPIPublisher.SendHSMMessage(wMsg, ref this._WAPIConfig);
                if (wResultLog.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    this._WAPIConfig.CurrenToken = null;
                    throw new Fwk.Exceptions.TechnicalException("Unauthorized");
                }
                if (wResultLog.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(wResultLog.StatusCode.ToString());
                if (wResultLog.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    wStrBld.AppendLine("UnsupportedMediaType");
                    wStrBld.AppendLine("Algun dato del json del requets " + Environment.NewLine);
                }
                else
                {
                    using (Stream stream = wResultLog.Content.ReadAsStreamAsync().Result)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        wHtml = reader.ReadToEnd();
                    }
                    wStrBld.AppendLine("StatusCode" + wResultLog.StatusCode);
                    if (wResultLog.StatusCode != System.Net.HttpStatusCode.OK)
                        //TODO: Log_old analizar si deberia hacerce un log aqui
                        this.Log_old("Error: " + wHtml, Fwk.Logging.EventType.Audit, false);
                }

                if (wHtml.Length > 0)
                {
                    wStatusResponse = (WhatsAppStatusBE)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(WhatsAppStatusBE), wHtml);
                    if (wStatusResponse.status == (int)System.Net.HttpStatusCode.BadRequest)
                    {
                        wStrBld.AppendLine("Body: " + wStatusResponse.errorData[0].message);
                        Exception ex = new Exception(wHtml);
                        throw ex;
                    }
                }

                wStrBld.AppendLine(Environment.NewLine);
                wTask = wResultLog.Content.ReadAsStringAsync();
                wContent = wTask.Result;
                //Actualizo la publicación.         
                if (!string.IsNullOrEmpty(wContent))
                {
                    wWhatsAppResult = (WhatsAppResultBE)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(WhatsAppResultBE), wContent);
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wWhatsAppResult.data[0].externalId);
                }
                else
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wTask.Id.ToString());

                //Si es encuesta actualiza la publication detail con el comentario con la url minificada
                if (this._WhatsAppConfig.WCShortenUrl && AccountInfo.ChannelType == PelsoftEnums.Channeltype.WhatsAppOnDemand)
                {
                    wJson = JsonConvert.SerializeObject(wMsg);
                    //wJson = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson_Newtonsoft(wMsg);
                    WhatsAppDAC.UpdatePublicationDetail(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, wJson);
                }
            }
            catch (WebException wex)
            {
                this.HandleWebExpection(wex, pPublish);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, pPublish);
            }
        }

        /// <summary>
        /// Metodo para enviar mensajes de texto y adjuntos
        /// </summary>
        /// <param name="pPublish"></param>
        public void SendDirectMessageWithAttachment(PublishBE pPublish, PublishAttachmentBE pAttachment, string pAttachment64)
        {
            StringBuilder wStrBld = new StringBuilder();
            string wHtml = string.Empty, wContent = string.Empty;
            WhatsAppStatusBE wStatusResponse = new WhatsAppStatusBE();
            HttpResponseMessage wResultLog = null;
            messageMultimedia wMsg = null;
            Task<string> wTask = null;
            WhatsAppResultBE wWhatsAppResult = null;

            try
            {
                wMsg = new messageMultimedia();
                //se usa en caso que sea messagetype="TEMPLATE"
                wMsg.messagetype = "MEDIA";
                if (!string.IsNullOrEmpty(pPublish.PublicationTo))
                {
                    wMsg.phones = new String[] { pPublish.PublicationTo };
                }
                wMsg.media = pAttachment64;
                wMsg.mediatype = pAttachment.PAttachmentMediaType;
                if (wMsg.mediatype != "PDF")
                    wMsg.text = pPublish.PComment;
                else
                    wMsg.text = pAttachment.PAttachmentName;

                wResultLog = WAPIPublisher.SendMultimediaMessage(wMsg, ref this._WAPIConfig);
                if (wResultLog.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    this._WAPIConfig.CurrenToken = null;
                    throw new Fwk.Exceptions.TechnicalException("Unauthorized");
                }

                if (wResultLog.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    wStrBld.AppendLine("UnsupportedMediaType");
                    wStrBld.AppendLine("Algun dato del json del requets " + Environment.NewLine);

                    //TODO: Log_old analizar si deberia hacerce un log aqui
                    this.Log_old("Error: " + wStrBld.ToString(), Fwk.Logging.EventType.Error, false);
                    throw new Exception("Error: " + wStrBld.ToString());
                }
                else
                {
                    using (Stream stream = wResultLog.Content.ReadAsStreamAsync().Result)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        wHtml = reader.ReadToEnd();
                    }
                    wStrBld.AppendLine("StatusCode" + wResultLog.StatusCode);
                    if (wResultLog.StatusCode != System.Net.HttpStatusCode.OK)
                        //TODO: Log_old analizar si deberia hacerce un log aqui
                        this.Log_old("Error: " + wHtml, Fwk.Logging.EventType.Error, false);
                }

                wTask = wResultLog.Content.ReadAsStringAsync();
                wContent = wTask.Result;
                //Actualizo la publicación.         
                if (!string.IsNullOrEmpty(wContent) && wContent.Contains("\"data\":["))
                {
                    wWhatsAppResult = (WhatsAppResultBE)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(WhatsAppResultBE), wContent);
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wWhatsAppResult.data[0].externalId);
                }
                else
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wTask.Id.ToString());
            }
            catch (WebException wex)
            {
                this.HandleWebExpection(wex, pPublish);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, pPublish);
            }
        }

        /// <summary>
        /// Para el envio de template a whatsaap.
        /// </summary>
        /// <param name="pPublish"></param>
        public void SendDirectMessageWithTemplate(PublishBE pPublish)
        {
            StringBuilder wStrBld = new StringBuilder();
            string wHtml = string.Empty, wJson = string.Empty, wContent = string.Empty;
            WhatsAppStatusBE wStatusResponse = new WhatsAppStatusBE();
            HttpResponseMessage wResultLog = null;
            messageHSM wMsg = null;
            Task<string> wTask = null;
            WhatsAppResultBE wWhatsAppResult = null;
            bool wHasJsonPub = (_PublishDetails != null && _PublishDetails.Any() && !string.IsNullOrEmpty(_PublishDetails.First().PDJsonPublication));

            try
            {
                if (wHasJsonPub)
                {
                    wJson = _PublishDetails.First().PDJsonPublication;
                    wMsg = (messageHSM)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(messageHSM),wJson);
                }
                else
                {
                    wMsg = new messageHSM();
                    wMsg.PublicationId = pPublish.PublicationId; //Se utiliza para el log del JSON.
                    if (!string.IsNullOrEmpty(pPublish.PublicationTo))
                        wMsg.phones = new String[] { pPublish.PublicationTo };
                    wMsg.messagetype = "TEMPLATE";
                    FillTemplateMessage(wMsg, AccountInfo.AsiConnectionStringDest, pPublish.CaseCommentGuid);
                }

                if (this._WhatsAppConfig.WCShortenUrl && AccountInfo.ChannelType == PelsoftEnums.Channeltype.WhatsAppOnDemand)
                {
                    for (int i = 0; i < wMsg.@params.Count(); i++)
                        wMsg.@params[i] = Helper.MinifyTextUrls(wMsg.@params[i]);
                }

                if (!wHasJsonPub)
                {
                    wJson = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson(wMsg);
                    WhatsAppDAC.UpdatePublicationDetail(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, wJson); //Se guarda un log antes del envío.
                }

                wResultLog = WAPIPublisher.SendHSMMessage(wMsg, ref this._WAPIConfig);
                if (wResultLog.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    this._WAPIConfig.CurrenToken = null;
                    throw new Fwk.Exceptions.TechnicalException("Unauthorized");
                }
                if (wResultLog.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(wResultLog.StatusCode.ToString());

                if (wResultLog.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    wStrBld.AppendLine("UnsupportedMediaType");
                    wStrBld.AppendLine("Algun dato del json del requets " + Environment.NewLine);
                }
                else
                {
                    using (Stream stream = wResultLog.Content.ReadAsStreamAsync().Result)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        wHtml = reader.ReadToEnd();
                    }
                    wStrBld.AppendLine("StatusCode" + wResultLog.StatusCode);
                    if (wResultLog.StatusCode != System.Net.HttpStatusCode.OK)
                        //TODO: Log_old analizar si deberia hacerce un log aqui
                        this.Log_old("Error: " + wHtml, Fwk.Logging.EventType.Audit, false);
                }
                if (wHtml.Length > 0)
                {
                    wStatusResponse = (WhatsAppStatusBE)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(WhatsAppStatusBE), wHtml);
                    if (wStatusResponse.status == (int)System.Net.HttpStatusCode.BadRequest)
                    {
                        wStrBld.AppendLine("Body: " + wStatusResponse.errorData[0].message);
                        Exception ex = new Exception(wHtml);
                        throw ex;
                    }
                }

                wStrBld.AppendLine(Environment.NewLine);
                wTask = wResultLog.Content.ReadAsStringAsync();
                wContent = wTask.Result;
                //Actualizo la publicación.         
                if (!string.IsNullOrEmpty(wContent))
                {
                    wWhatsAppResult = (WhatsAppResultBE)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson(typeof(WhatsAppResultBE), wContent);
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wWhatsAppResult.data[0].externalId);
                }
                else
                    PublishDAC.UpdatePublish(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, DateTime.Now, this.currentProcessDetailsID, Convert.ToInt32(this._ApplicationSettings.Value), wTask.Id.ToString());
                //Si es encuesta actualiza la publication detail con el comentario con la url minificada
                if (this._WhatsAppConfig.WCShortenUrl && AccountInfo.ChannelType == PelsoftEnums.Channeltype.WhatsAppOnDemand)
                {
                    wJson = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson(wMsg);
                    WhatsAppDAC.UpdatePublicationDetail(AccountInfo.AsiConnectionStringDest, pPublish.PublicationId, wJson);
                }
            }
            catch (WebException wex)
            {
                this.HandleWebExpection(wex, pPublish);
                string resp = string.Empty;
            }
            catch (Exception ex)
            {
                this.HandleException(ex, pPublish);
            }
        }
        #endregion

        #region << -Manejo de Excepciones- >>
        /// <summary>
        /// Maneja las excepciones web
        /// </summary>
        /// <param name="pWex"></param>
        /// <param name="pPublish"></param>
        private void HandleWebExpection(WebException pWex, PublishBE pPublish)
        {
            string wRes = string.Empty;
            PublishErrorBE wPError = null;

            try
            {
                wRes = new StreamReader(pWex.Response.GetResponseStream()).ReadToEnd();
                wPError = PublishHelper.ManageError(AccountInfo, AccountInfo.ChannelType, pWex, pPublish, this._ApplicationSettings.Value, wRes);

                if (wPError.PEAlertedByService)
                    Log(pWex);
            }
            catch { }
        }

        /// <summary>
        /// Maneja las excepciones en general
        /// </summary>
        /// <param name="pExc"></param>
        /// <param name="pPublish"></param>
        private void HandleException(Exception pExc, PublishBE pPublish)
        {
            PublishErrorBE wPublishError = PublishHelper.ManageError(AccountInfo, AccountInfo.ChannelType, pExc, pPublish, this._ApplicationSettings.Value, string.Empty);
            if (wPublishError.PEAlertedByService)
                this.Log(pExc);
        }
        #endregion
    }
}
