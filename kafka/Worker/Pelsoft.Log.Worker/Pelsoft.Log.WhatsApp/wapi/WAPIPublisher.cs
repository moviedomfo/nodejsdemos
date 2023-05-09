using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Pelsoft.Log.WhatsApp.wapi;
using Fwk.HelperFunctions;


namespace konectaAPI.wapi
{
    public class WAPIPublisher
    {
        private static HttpResponseMessage ExceptionHttpResponseMessage(Exception ex, string pUrl)
        {
            string wMsg = string.Empty;
            HttpResponseMessage wResponse = null;

            if (ex.InnerException != null)
            {
                wMsg = ex.InnerException.Message;
                if (ex.InnerException.GetType() == typeof(System.Net.Sockets.SocketException))
                {
                    if ((ex.InnerException as System.Net.Sockets.SocketException).ErrorCode == 10060)
                        wMsg = string.Format("{0} no es accesible. Error: {2}", pUrl, wMsg);
                }
            }
            else
                wMsg = ex.Message;

            wResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(wMsg),
                ReasonPhrase = wMsg
            };
            return wResponse;
        }

        /// <summary>
        /// Envía un POST con un json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <param name="pWAPIConnectorConfig"></param>
        /// <returns></returns>
        private static HttpResponseMessage SendPostRequest<T>(T pObject, string pUrl, WebProxy pProxy, string pAuthenticationToken = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage wRequestResponse = null;
            HttpClientHandler wClientHandler = new HttpClientHandler();
            StringContent wContent = null;
            string wJson = string.Empty;

            try
            {
                wClientHandler.Proxy = pProxy;
                wClientHandler.UseProxy = true;
                wClientHandler.PreAuthenticate = false;
                wClientHandler.UseDefaultCredentials = true;

                using (HttpClient wHttpClient = new HttpClient(wClientHandler))
                {
                    wJson = Fwk.HelperFunctions.SerializationFunctions.SerializeObjectToJson(pObject);
                    wContent = new StringContent(wJson, Encoding.UTF8, "application/json");

                    wHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(pAuthenticationToken))
                        wHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("AuthenticationToken", pAuthenticationToken);

                    wRequestResponse = wHttpClient.PostAsync(pUrl, wContent).Result;
                }
            }
            catch (Exception ex)
            {
                return ExceptionHttpResponseMessage(ex, pUrl);
            }

            return wRequestResponse;
        }

        /// <summary>
        /// Envía un POST con un json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObject"></param>
        /// <param name="pWAPIConnectorConfig"></param>
        /// <returns></returns>
        private static HttpResponseMessage SendPostRequest(string pUrl, WebProxy pProxy, string pAuthenticationToken = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebResponse wResponse = null;
            HttpWebRequest wRequest = null;
            HttpResponseMessage wHttpResponseMessage = null;
            string wResponseString = string.Empty;

            try
            {
                wRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                wRequest.Method = "POST";
                wRequest.Accept = "application/json";
                wRequest.PreAuthenticate = false;
                wRequest.ContentType = @"application/json; charset=utf-8";
                wRequest.ContentLength = 0;

                wRequest.Proxy = pProxy;

                wResponse = (HttpWebResponse)wRequest.GetResponse();
                wResponseString = new StreamReader(wResponse.GetResponseStream()).ReadToEnd();

                wHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(wResponseString, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                return ExceptionHttpResponseMessage(ex, pUrl);
            }

            return wHttpResponseMessage;
        }

        private static WebProxy GetProxy(WAPIConnectorConfig pWAPIConnectorConfig)
        {
            Uri wUri = null;
            WebProxy wProxy = null;

            if (!pWAPIConnectorConfig.ProxyEnabled)
                return null;

            wUri = new Uri(string.Format("http://{0}:{1}", pWAPIConnectorConfig.ProxyName, pWAPIConnectorConfig.ProxyPort));

            wProxy = new WebProxy(wUri);
            wProxy.Credentials = new System.Net.NetworkCredential(pWAPIConnectorConfig.ProxyUser, pWAPIConnectorConfig.ProxyPassword, pWAPIConnectorConfig.ProxyDomain);

            return wProxy;
        }

        /// <summary>
        /// Obtiene el token de autenticación
        /// </summary>
        /// <param name="pWAPIConnectorConfig"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetAuthenticationToken(ref WAPIConnectorConfig pWAPIConnectorConfig)
        {
            HttpResponseMessage wRes = null;
            string wUrl = string.Empty, wAsyncContent = string.Empty;
            WebProxy wProxy = null;
            authResponse wAuthResponse = null;

            if (string.IsNullOrEmpty(pWAPIConnectorConfig.CurrenToken) || DateTime.Now.AddHours(1) > pWAPIConnectorConfig.ExpiredDate)
            {
                wUrl = string.Format("{0}rest/authenticate/login?user={1}&password={2}", pWAPIConnectorConfig.Url, pWAPIConnectorConfig.User, pWAPIConnectorConfig.Password);
                wProxy = GetProxy(pWAPIConnectorConfig);

                wRes = SendPostRequest(wUrl, wProxy);
                if (wRes.StatusCode == HttpStatusCode.OK)
                {
                    wAsyncContent = wRes.Content.ReadAsStringAsync().Result;
                    wAuthResponse = (authResponse)SerializationFunctions.DeSerializeObjectFromJson(typeof(authResponse), wAsyncContent);
                    pWAPIConnectorConfig.CurrenToken = wAuthResponse.token;
                    pWAPIConnectorConfig.ExpiredDate = DateTime.Now.AddDays(1);
                }
            }
            else
                wRes = new HttpResponseMessage(HttpStatusCode.OK);

            return wRes;
        }

        /// <summary>
        /// Envía una plantilla HSM
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pWAPIConnectorConfig"></param>
        /// <returns></returns>
        public static HttpResponseMessage SendHSMMessage(messageHSM pMessage, ref WAPIConnectorConfig pWAPIConnectorConfig)
        {
            WebProxy wProxy = GetProxy(pWAPIConnectorConfig);
            string wUrl = string.Format("{0}rest/services/send", pWAPIConnectorConfig.Url);
            HttpResponseMessage wRes = GetAuthenticationToken(ref pWAPIConnectorConfig);
            if (wRes.StatusCode != HttpStatusCode.OK)
                return wRes;

            return SendPostRequest<messageHSM>(pMessage, wUrl, wProxy, pWAPIConnectorConfig.CurrenToken);
        }

        //public static HttpResponseMessage FindMessageByPhone(string pPhone, int pLimit, ref WAPIConnectorConfig pWAPIConnectorConfig, out findByPhoneResponse pResultPhones)
        //{
        //    WebProxy wProxy = GetProxy(pWAPIConnectorConfig);
        //    string wUrl = string.Format("{0}rest/services/find?phone={1}&limit={2}", pWAPIConnectorConfig.Url, pPhone, pLimit);
        //    string wStrResponse = string.Empty;
        //    HttpResponseMessage wRes = GetAuthenticationToken(ref pWAPIConnectorConfig);
        //    if (wRes.StatusCode != HttpStatusCode.OK)
        //    {
        //        pResultPhones = null;
        //        return wRes;
        //    }

        //    wRes = SendPostRequest(wUrl, wProxy, pWAPIConnectorConfig.CurrenToken);
        //    wStrResponse = wRes.Content.ReadAsStringAsync().Result;
        //    pResultPhones = (findByPhoneResponse)Fwk.HelperFunctions.SerializationFunctions.DeSerializeObjectFromJson_Newtonsoft(typeof(findByPhoneResponse), wStrResponse);

        //    return wRes;
        //}

        /// <summary>
        /// Envía mensajes multimedia
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pWAPIConfig"></param>
        /// <returns></returns>
        public static HttpResponseMessage SendMultimediaMessage(messageMultimedia pMessage, ref WAPIConnectorConfig pWAPIConfig)
        {
            WebProxy wProxy = GetProxy(pWAPIConfig);
            string wUrl = string.Format("{0}rest/services/send", pWAPIConfig.Url);
            HttpResponseMessage wRes = GetAuthenticationToken(ref pWAPIConfig);
            if (wRes.StatusCode != HttpStatusCode.OK)
                return wRes;

            return SendPostRequest<messageMultimedia>(pMessage, wUrl, wProxy, pWAPIConfig.CurrenToken);
        }

        //public static HttpResponseMessage TransferConversationWS(WhatsAppTransferConversation pWtspTransferConversation, ref WAPIConnectorConfig pWAPIConfig)
        //{
        //    WebProxy wProxy = GetProxy(pWAPIConfig);
        //    string wUrl = string.Format("{0}rest/services/transfer-conversation", pWAPIConfig.Url);
        //    HttpResponseMessage wRes = GetAuthenticationToken(ref pWAPIConfig);
        //    if (wRes.StatusCode != HttpStatusCode.OK)
        //        return wRes;

        //    return SendPostRequest<WhatsAppTransferConversation>(pWtspTransferConversation, wUrl, wProxy, pWAPIConfig.CurrenToken);
        //}
    }
}
