//using Fwk.HelperFunctions;
//using rapiAPI.wapi.Common.BE;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Timers;



//namespace rapiAPI.wapi
//{
//    public class WAPIConnector
//    {
//        public static Fwk.Caching.FwkSimpleStorageBase2<MessageBotList> msmq = null;
//        public static Fwk.Caching.FwkSimpleStorageBase2<messageRecivedBEList> msmq_errors = null;

//        public static string currenToken { get; set; }
//        public static DateTime ExpiredDate { set; get; }
//        static WebProxy proxy;
//        static bool deenqueuerStarted = false;

//        public static bool DeenqueuerStarted
//        {
//            get { return WAPIConnector.deenqueuerStarted; }
//            set { WAPIConnector.deenqueuerStarted = value; }
//        }

//        public static apiConfig apiConfig
//        {
//            get { return wapiHelper.apiConfig; }
//            set { wapiHelper.apiConfig = value; }
//        }

//        static WAPIConnector()
//        {


//            if (wapiHelper.apiConfig == null)
//                wapiHelper.apiConfig = new apiConfig();

//            msmq = new Fwk.Caching.FwkSimpleStorageBase2<MessageBotList>("st1");
//            msmq_errors = new Fwk.Caching.FwkSimpleStorageBase2<messageRecivedBEList>("st2");
//            if (msmq.StorageObject == null)
//                msmq.StorageObject = new MessageBotList();

//            if (msmq_errors.StorageObject == null)
//                msmq_errors.StorageObject = new messageRecivedBEList();

//        }


//        static WebProxy get_proxy()
//        {

//            if (wapiHelper.apiConfig.proxyEnabled == false)
//                return null;

//            if (proxy == null)
//            {
//                var proxyURI = new Uri(string.Format("http://{0}:{1}", wapiHelper.apiConfig.proxyName, wapiHelper.apiConfig.proxyPort));
//                //ICredentials credentials = new NetworkCredential(wapiHelper.apiConfig.proxyUser, wapiHelper.apiConfig.proxyPassword, "allus-ar");
//                //proxy = new WebProxy(proxyURI, true, null, credentials);

//                proxy = new WebProxy(proxyURI);
//                //proxy.Credentials = new System.Net.NetworkCredential("moviedo", "rapi+45", "allus-ar");
//                proxy.Credentials = new System.Net.NetworkCredential(wapiHelper.apiConfig.proxyUser, wapiHelper.apiConfig.proxyPassword, wapiHelper.apiConfig.proxyDomain);

//            }


//            return proxy;
//        }

//        /// <summary>
//        /// Usa las confuguraciones de apiConfig.json
//        /// estavblece el apiConfig.currenToken refrescado
//        /// </summary>
//        /// <returns></returns>
//        public static HttpResponseMessage loging()
//        {
//            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

//            HttpResponseMessage res;
//            if (WAPIConnector.currenToken == null || DateTime.Now.AddHours(1) > WAPIConnector.ExpiredDate)
//                res = loging(wapiHelper.apiConfig.api_user, wapiHelper.apiConfig.api_password, wapiHelper.apiConfig.proxyEnabled);
//            else
//            {
//                res = new HttpResponseMessage(HttpStatusCode.OK);
//                return res;
//            }


//            if (res.StatusCode == HttpStatusCode.OK)
//            {
//                var asyncContent = res.Content.ReadAsStringAsync().Result;
//                authResponse authResponse = (authResponse)SerializationFunctions.DeSerializeObjectFromJson_Newtonsoft(typeof(authResponse), asyncContent);
//                WAPIConnector.currenToken = authResponse.token;
//                WAPIConnector.ExpiredDate = DateTime.Now.AddDays(1);

//            }
//            return res;

//        }

//        public static HttpResponseMessage loging(ref WAPIConnectorConfig pWAPIConnectorConfig)
//        {
//            HttpResponseMessage wRes = null;
//            if (string.IsNullOrEmpty(pWAPIConnectorConfig.CurrenToken) || DateTime.Now.AddHours(1) > pWAPIConnectorConfig.ExpiredDate)
//                wRes = loging(pWAPIConnectorConfig.User, pWAPIConnectorConfig.Password, pWAPIConnectorConfig.Url, pWAPIConnectorConfig.ProxyEnabled);
//            else
//                return new HttpResponseMessage(HttpStatusCode.OK);

//            if (wRes.StatusCode == HttpStatusCode.OK)
//            {
//                var asyncContent = wRes.Content.ReadAsStringAsync().Result;
//                authResponse authResponse = (authResponse)SerializationFunctions.DeSerializeObjectFromJson_Newtonsoft(typeof(authResponse), asyncContent);
//                pWAPIConnectorConfig.CurrenToken = authResponse.token;
//                pWAPIConnectorConfig.ExpiredDate = DateTime.Now.AddDays(1);

//            }
//            return wRes;
//        }

//        /// <summary>
//        /// from postamn http://localhost:51528/api/wapi/loging?user=moviedo&password=12345
//        /// </summary>
//        /// <param name="user"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public static HttpResponseMessage loging(string user, string password, bool pProxyEnabled)
//        {
//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var wUrl = string.Format("{0}rest/authenticate/login?user={1}&password={2}", wapiHelper.apiConfig.apiDomain, user, password);
//            // var url = string.Format("{0}api/wapi/loging?user={1}&password={2}", wapiHelper.apiConfig.apiDomain, user, password);

//            try
//            {
//                var request = (HttpWebRequest)WebRequest.Create(wUrl);
//                request.Method = "POST";
//                request.Accept = "application/json";
//                request.PreAuthenticate = false;
//                request.ContentType = @"application/json; charset=utf-8";
//                request.ContentLength = 0;

//                if (pProxyEnabled)
//                    request.Proxy = get_proxy();

//                var response = (HttpWebResponse)request.GetResponse();
//                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

//                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
//                {
//                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
//                };

//                return httpResponseMessage;
//            }

//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }
//        }

//        public static HttpResponseMessage loging(string pUser, string pPassword, string pUrl, bool pProxyEnabled)
//        {
//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var url = string.Format("{0}rest/authenticate/login?user={1}&password={2}", pUrl, pUser, pPassword);
//            // var url = string.Format("{0}api/wapi/loging?user={1}&password={2}", wapiHelper.apiConfig.apiDomain, user, password);

//            try
//            {
//                var request = (HttpWebRequest)WebRequest.Create(url);
//                request.Method = "POST";
//                request.Accept = "application/json";
//                request.PreAuthenticate = false;
//                request.ContentType = @"application/json; charset=utf-8";
//                request.ContentLength = 0;

//                if(pProxyEnabled)
//                    request.Proxy = get_proxy();

//                var response = (HttpWebResponse)request.GetResponse();
//                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

//                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
//                {
//                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
//                };

//                return httpResponseMessage;
//            }

//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }
//        }

//        private static HttpClient GetHttpClient()
//        {
//            HttpClient wHttpClient = null;
//            HttpClientHandler wHttpClientHandler = wapiHelper.getProxy_HttpClientHandler();

//            if (wHttpClientHandler != null)
//                wHttpClient = new HttpClient(wHttpClientHandler);
//            else
//                wHttpClient = new HttpClient();

//            return wHttpClient;
//        }

//        /// <summary>
//        /// hace un send a rest/services/send
//        /// </summary>
//        /// <param name="message"></param>
//        /// <returns>to read result text  use 
//        /// responseString = await response.Content.ReadAsStringAsync(); 
//        /// </returns>
//        public static async Task<HttpResponseMessage> senMessagedHSM_async(messageHSM message)
//        {
//            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var url = string.Format("{0}rest/services/send", wapiHelper.apiConfig.apiDomain);
//            //var url = string.Format("{0}api/wapi/senMessagedHSM_async", wapiHelper.apiConfig.apiDomain);

//            try
//            {
//                using (var httpClient = GetHttpClient())
//                {
//                    string jsonContnt = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson_Newtonsoft(message);
//                    StringContent content = new StringContent(jsonContnt, Encoding.UTF8, "application/json");
//                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", WAPIConnector.currenToken);

//                    var response = await httpClient.PostAsync(url, content);

//                    return response;
//                }
//            }
//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }
//        }

//        public static async Task<HttpResponseMessage> senMessagedHSM_async(messageHSM message, WAPIConnectorConfig pWAPIConfig)
//        {
//            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var url = string.Format("{0}rest/services/send", pWAPIConfig.Url);

//            try
//            {
//                using (var httpClient = GetHttpClient())
//                {
//                    string jsonContnt = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson_Newtonsoft(message);
//                    StringContent content = new StringContent(jsonContnt, Encoding.UTF8, "application/json");
//                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", pWAPIConfig.CurrenToken);

//                    var response = await httpClient.PostAsync(url, content);

//                    return response;
//                }
//            }
//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }
//        }

//        #region
//        public static async Task<findByPhoneResponse> findMessaged_by_phone_async(string phone, int limit)
//        {
//            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var url = string.Format("{0}rest/services/find?phone={1}&limit={2}", wapiHelper.apiConfig.apiDomain, phone, limit);
//            var request = (HttpWebRequest)WebRequest.Create(url);
//            try
//            {
//                var httpHandler = wapiHelper.getProxy_HttpClientHandler();
//                using (var httpClient = new HttpClient())
//                {
//                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", WAPIConnector.currenToken);
//                    var response = await httpClient.GetAsync(url);
//                    var strResult = await response.Content.ReadAsStringAsync();
//                    var res = (findByPhoneResponse)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson_Newtonsoft(typeof(findByPhoneResponse), strResult);
//                    return res;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw WAPIConnector.getHttpResponseException(ex);
//            }
//        }
//        #endregion


//        public static HttpResponseMessage senMessagedHSM(messageHSM message, bool wProxyEnabled)
//        {
//            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            try
//            {
//                var url = string.Format("{0}rest/services/send", wapiHelper.apiConfig.apiDomain);
//                var request = (HttpWebRequest)WebRequest.Create(url);
//                request.Method = "POST";
//                request.Accept = "application/json";
//                request.PreAuthenticate = false;
//                request.ContentType = "application/json; charset=utf-8";
//                request.Headers = new WebHeaderCollection();
//                request.Headers.Add("AuthenticationToken", WAPIConnector.currenToken);

//                if (wProxyEnabled)
//                    request.Proxy = get_proxy();

//                //Set body
//                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
//                {
//                    string jsonContnt = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson_Newtonsoft(message);
//                    streamWriter.Write(jsonContnt);
//                    streamWriter.Flush();
//                    streamWriter.Close();
//                }

//                string responseString;
//                var httpResponse = (HttpWebResponse)request.GetResponse();
//                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
//                {
//                    responseString = streamReader.ReadToEnd();
//                }

//                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
//                {
//                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
//                };

//                return httpResponseMessage;
//            }
//            catch (WebException er)
//            {
//                var resp = new StreamReader(er.Response.GetResponseStream()).ReadToEnd();
//                return getHttpResponseMessage(er);
//            }
//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }
//        }

//        #region Hook
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="message"></param>
//        /// <param name="accountPhoneNumber"></param>
//        /// <returns></returns>
//        public static HttpResponseMessage reciveMessage(messageRecived message, string userPhone, string accountPhone)
//        {
//            #region validations
//            StringBuilder str = new StringBuilder();


//            if (string.IsNullOrEmpty(accountPhone))
//                str.AppendLine("accountPhone in url param " + " Es requerido");

//            if (string.IsNullOrEmpty(userPhone))
//                str.AppendLine("userPhone in url param " + " Es requerido");

//            if (string.IsNullOrEmpty(message.externalId))
//                str.AppendLine("externalId (WMessageBotExternalId)" + " Es requerido");

//            messageRecivedBE messageRecivedBE = (messageRecivedBE)message;
//            messageRecivedBE.api_userPhopne = userPhone;
//            messageRecivedBE.api_accountPhopne = accountPhone;// +1 (438) 476-3261 smooth



//            if (str.Length != 0)
//            {


//                var resp = new HttpResponseMessage(HttpStatusCode.PartialContent)
//                {
//                    Content = new StringContent(str.ToString()),
//                };
//                messageRecivedBE.api_errorMessage = str.ToString();
//                WAPIConnector.msmq_Enqueue_errors(messageRecivedBE);
//                return resp;

//            }

//            #endregion
//            MessageBotBE messageBE = new MessageBotBE();
//            try
//            {

//                #region Mapping BE
//                //exto del mensaje
//                messageBE.WMessageBotMessage = message.text;

//                messageBE.WMessageBotCreatedDate = message.fechaCreacion;

//                //WMessageBotExternalId [NOT NULL]: identificador único del mensaje,  proveniente de Whatsapp. 
//                messageBE.WMessageBotExternalId = message.externalId;

//                ///TODO: si este hook se reutilizara se debera configurar un parametro estatico
//                messageBE.WWhatsAppId = accountPhone;// "541130473326";
//                messageBE.WMessageBotPhone = userPhone;


//                //WMessageBotAttachmentUrl [NULL] (*): URL del adjunto que viene en el mensaje 
//                //WMessageBotTypeAttachment [NULL] (*): tipo de adjunto.

//                //WMessageBotProcessDate [NULL]: fecha y hora de procesamiento por parte del servicio de log
//                messageBE.WMessageBotCreatedDate = message.fechaCreacion;


//                if (message.deliveredDate.HasValue)
//                    messageBE.WMessageBotDeliveredDate = message.deliveredDate.Value;


//                //WMessageBotReadDate [NULL]: fecha/hora de lectura del mensaje
//                if (message.readDate.HasValue)
//                    messageBE.WMessageBotReadDate = message.readDate.Value;

//                //WMessageBotOrigin [NOT NULL]: origen del mensaje. Puede tener los siguientes valores:   ACCOUNT: proviene de la cuenta del cliente   USER: proviene de un usuario final
//                messageBE.WMessageBotOrigin = message.messageorigin;


//                //WMessageBotStatus [NOT NULL]: estado del mensaje. Puede tener los siguientes valores:
//                //  SEND: enviado  DELIVERED: recibido  READ: leído
//                messageBE.WMessageBotStatus = message.messagestatus;


//                //WMessageBotType [NOT NULL]: tipo de mensaje. Puede tener los siguientes valores: TEXT MEDIA TEMPLATE
//                messageBE.WMessageBotType = message.messagetype;
//                if (message.@params != null)
//                {
//                    if (message.@params.Length != 0)
//                    {
//                        var strParams = Fwk.HelperFunctions.FormatFunctions.GetStringBuilderWhitSeparator(message.@params.ToList(), ';');
//                        messageBE.WMessageBotParam = strParams.ToString();
//                    }
//                }

//                //WMessageBotTemplate [NULL]: nombre de la plantilla usada para envío de mensajes. Ejemplo: "chatclub_southcone_welcome_v1"
//                messageBE.WMessageBotTemplate = message.template;
//                //WMessageBotNameSpace [NULL]: identificador único de la plantilla. Ejemplo: "1e1a3508_c587_e845_d431_b6764aae2e52"
//                messageBE.WMessageBotNameSpace = message.@namespace;

//                //WMessageBotRedirected [NOT NULL]: Indica si el mensaje fue renviado o no.
//                messageBE.WMessageBotRedirected = message.redirected;




//                #endregion

//                //si hay errores de negocio 
//                if (str.Length != 0)
//                {
//                    messageBE.api_errorMessage = str.ToString();
//                    WAPIConnector.msmq_Enqueue(messageBE);
//                }
//                else
//                {

//                    Task t = TryInsert(messageBE);

//                }


//                var resp = new HttpResponseMessage(HttpStatusCode.OK)
//                {
//                    Content = new StringContent("message resived"),
//                };


//                return resp;
//            }
//            catch (Exception ex)
//            {
//                messageRecivedBE.api_errorMessage = Fwk.Exceptions.ExceptionHelper.GetAllMessageException(ex);
//                WAPIConnector.msmq_Enqueue_errors(messageRecivedBE);
//                return getHttpResponseMessage(ex);
//            }

//        }
//        static Task TryInsert(MessageBotBE messageBot)
//        {
//            return Task.Run(() =>
//            {
//                try
//                {
//                    rapiAPI.wapi.DAC.rapiAPIDAC.Insert(messageBot);
//                }
//                catch (Exception ex)
//                {
//                    messageBot.api_errorMessage = Fwk.Exceptions.ExceptionHelper.GetAllMessageException(ex);
//                    WAPIConnector.msmq_Enqueue(messageBot);
//                    //throw Fwk.Exceptions.ExceptionHelper.ProcessException(ex);
//                }


//            });
//        }
//        #endregion


//        #region helpers
//        public static HttpResponseMessage getHttpResponseMessage(Exception ex)
//        {
//            string msg = string.Empty;

//            if (ex.InnerException != null)
//            {

//                msg = ex.InnerException.Message;
//                if (ex.InnerException.GetType() == typeof(System.Net.Sockets.SocketException))
//                {
//                    var e = ex.InnerException as System.Net.Sockets.SocketException;
//                    if (e.ErrorCode == 10060)
//                        msg = wapiHelper.apiConfig.apiDomain + " no es accesible " + msg;
//                }
//            }

//            else
//                msg = ex.Message;
//            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
//            {
//                Content = new StringContent(msg),
//                ReasonPhrase = msg
//            };
//            return resp;
//        }

//        public static HttpResponseMessage getHttpResponseMessage(Exception ex, string pUrl)
//        {
//            string msg = string.Empty;

//            if (ex.InnerException != null)
//            {

//                msg = ex.InnerException.Message + " - URL: " + pUrl;
//                if (ex.InnerException.GetType() == typeof(System.Net.Sockets.SocketException))
//                {
//                    var e = ex.InnerException as System.Net.Sockets.SocketException;
//                    if (e.ErrorCode == 10060)
//                        msg = wapiHelper.apiConfig.apiDomain + " no es accesible " + msg;
//                }
//            }

//            else
//                msg = ex.Message + " - URL: " + pUrl;
//            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
//            {
//                Content = new StringContent(msg),
//                ReasonPhrase = msg
//            };
//            return resp;
//        }


//        public static HttpResponseException getHttpResponseException(Exception ex)
//        {

//            if (ex.GetType() == typeof(HttpResponseException))
//                return ex as HttpResponseException;

//            string msg = string.Empty;
//            if (ex.InnerException != null)
//            {

//                msg = ex.InnerException.Message;
//                if (ex.InnerException.GetType() == typeof(System.Net.Sockets.SocketException))
//                {
//                    var e = ex.InnerException as System.Net.Sockets.SocketException;
//                    if (e.ErrorCode == 10060)
//                        //msg = wapiHelper.apiConfig.apiDomain + " no es accesible " + Environment.NewLine + msg;
//                        msg = wapiHelper.apiConfig.apiDomain + " no es accesible. " + msg;
//                }

//            }
//            else
//                msg = ex.Message;
//            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
//            {
//                Content = new StringContent(msg),
//                ReasonPhrase = msg
//            };
//            return new HttpResponseException(resp);
//        }

//        #endregion


//        #region MSMQ
//        public static void msmq_Enqueue(MessageBotBE item)
//        {

//            Monitor.Enter(msmq.StorageObject);
//            item.api_enqueuedTime = DateTime.Now;
//            msmq.StorageObject.Add(item);
//            msmq.Save();
//            Monitor.Exit(msmq.StorageObject);
//        }
//        public static void msmq_Enqueue_errors(messageRecivedBE item)
//        {

//            Monitor.Enter(msmq_errors.StorageObject);
//            item.api_enqueuedTime = DateTime.Now;
//            msmq_errors.StorageObject.Add(item);
//            msmq_errors.Save();
//            Monitor.Exit(msmq_errors.StorageObject);
//        }

//        public static messageRecivedBEList msmq_errors_get()
//        {

//            return msmq_errors.StorageObject;
//        }

//        public static MessageBotBEList_json msmq_get()
//        {
//            MessageBotBEList_json list = new MessageBotBEList_json(msmq.StorageObject);
//            return list;
//        }

//        static System.Timers.Timer timer;

//        public static void Start_Dequeuer()
//        {


//            timer = new System.Timers.Timer(10 * 1000);
//            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

//            timer.Start();
//            deenqueuerStarted = true;
//        }

//        static void timer_Elapsed(object sender, ElapsedEventArgs e)
//        {

//            timer.Stop();

//            try
//            {
//                Monitor.Enter(WAPIConnector.msmq.StorageObject);

//                var msmqSorted = WAPIConnector.msmq.StorageObject.OrderBy(p => p.WMessageBotDeliveredDate);
//                var lote = msmqSorted.Take(5);

//                msmqSorted.ToList().ForEach(item =>
//                {

//                    WAPIConnector.msmq_removeFrom(item);
//                    try
//                    {
//                        //si ocurre error simplemente deja el msg en la cola
//                        rapiAPI.wapi.DAC.rapiAPIDAC.Insert(item);
//                    }
//                    catch { }

//                });

//                Monitor.Exit(WAPIConnector.msmq.StorageObject);
//            }

//            catch (Exception te)
//            {
//                //ProcessEx(te);
//            }
//            finally
//            {
//                if (timer != null)
//                    timer.Start();

//            }
//        }

//        public static void Stop_Dequeuer()
//        {
//            deenqueuerStarted = false;
//            timer.Stop();
//            timer.Elapsed -= new ElapsedEventHandler(timer_Elapsed);
//            timer = null;

//        }



//        public static void msmq_removeFrom(MessageBotBE item)
//        {
//            var s = msmq.StorageObject.Where(p => p.WWhatsAppId == item.WWhatsAppId).FirstOrDefault();
//            if (s != null)
//                msmq.StorageObject.Remove(s);
//        }

//        public static MessageBotBEList_json msmq_clean()
//        {
//            msmq = new Fwk.Caching.FwkSimpleStorageBase2<MessageBotList>("st1");



//            msmq.StorageObject = null;
//            msmq.StorageObject = new MessageBotList();
//            msmq.Save();
//            msmq.Load();




//            MessageBotBEList_json list = new MessageBotBEList_json(msmq.StorageObject);

//            return list;
//        }

//        public static MessageBotBEList_json msmqError_clean()
//        {

//            msmq_errors = new Fwk.Caching.FwkSimpleStorageBase2<messageRecivedBEList>("st2");





//            msmq_errors.StorageObject = null;
//            msmq_errors.StorageObject = new messageRecivedBEList();
//            msmq_errors.Save();
//            msmq_errors.Load();

//            MessageBotBEList_json list = new MessageBotBEList_json(msmq.StorageObject);

//            return list;
//        }

//        public static messageRecivedBEList msmq_errors_clean()
//        {

//            msmq_errors.StorageObject.Clear();



//            return msmq_errors.StorageObject;
//        }
//        #endregion


//        /// <summary>
//        /// hace un send con adjuntos a rest/services/send
//        /// </summary>
//        /// <param name="message"></param>
//        /// <returns>to read result text  use 
//        /// responseString = await response.Content.ReadAsStringAsync(); 
//        /// </returns>
//        public static async Task<HttpResponseMessage> senMessagedMultimedia_async(messageMultimedia message, WAPIConnectorConfig pWAPIConfig)
//        {
//            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            var url = string.Format("{0}rest/services/send", pWAPIConfig.Url);

//            // var url = string.Format("{0}api/wapi/senMessagedMultimedia_async", wapiHelper.apiConfig.apiDomain);

//            try
//            {
//                using (var httpClient = GetHttpClient())
//                {
//                    string jsonContnt = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson_Newtonsoft(message);
//                    StringContent content = new StringContent(jsonContnt, Encoding.UTF8, "application/json");


//                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", pWAPIConfig.CurrenToken);

//                    var response = await httpClient.PostAsync(url, content);

//                    return response;
//                }
//            }
//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//                //throw WAPIConnector.getHttpResponseException(ex);
//            }
//        }

//        public static async Task<HttpResponseMessage> TransferConversationWSAsync(WhatsAppTransferConversation pWtspTransferConversation)
//        {
//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//            HttpResponseMessage wHttpResp = null;
//            string URL = string.Format("{0}rest/services/transfer-conversation", wapiHelper.apiConfig.apiDomain);

//            try
//            {
//                using (HttpClient wHttpClient = GetHttpClient())
//                {
//                    string wJsonContent = SerializationFunctions.SerializeObjectToJson_Newtonsoft(pWtspTransferConversation);
//                    StringContent wContent = new StringContent(wJsonContent, Encoding.UTF8, "application/json");
//                    wHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    wHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", currenToken);

//                    wHttpResp = await wHttpClient.PostAsync(URL, wContent);
//                }
//            }
//            catch (Exception ex)
//            {
//                return getHttpResponseMessage(ex);
//            }

//            return wHttpResp;
//        }
//    }
//}

