using Fwk.Exceptions;
using Fwk.HelperFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.wapi
{
    ///Entidades que vienen de la api : estas no son marcadas como serializable dado que sopotan json y 
    ///si lo estarian el json sufriria deformaciones en sus atributos

    #region
    public class authResponse
    {

        public string token { get; set; }
        public string status { get; set; }
        public string code { get; set; }
    }


    /// <summary>
    /// Este msg viene del hook
    /// </summary>
    public class messageRecived
    {
        /// <summary>
        /// Es el identificador externo del proveedor.
        /// </summary>
        public string externalId { get; set; }

        /// <summary>
        /// Es la fecha en la que se ha enviado el mensaje.
        /// </summary>
        public long? deliveredDate { get; set; }

        /// <summary>
        /// es la fecha en la que el mensaje se crea y se guarda en BD
        /// </summary>
        public long? fechaCreacion { get; set; }

        /// <summary>
        /// Es la fecha en la que se ha leído el mensaje
        /// </summary>
        public long? readDate { get; set; }
        /// <summary>
        /// Identifica si el mensaje ha sido enviado por una cuenta (Account), como por ejemplo KCRM, o bien ha sido el usuario (USER).
        /// </summary>
        public string messageorigin { get; set; }

        /// <summary>
        /// Identifica el estado en el que se encuentra el mensaje.
        /// </summary>
        public string messagestatus { get; set; }

        /// <summary>
        /// tipo de mensaje
        /// </summary>
        public string messagetype { get; set; }

        /// <summary>
        /// Identifica si el mensaje ha sido reenviado o no
        /// </summary>
        public bool redirected { get; set; }

        /// <summary>
        /// Contiene el cuerpo del mensaje. 
        /// </summary>
        public string text { get; set; }

        public string template { get; set; }
        //Es el identificador de la plantilla, para el ejemplo: "1e1a3508_c587_e845_d431_b6764aae2e52"
        public string @namespace { get; set; }
        public string[] @params { get; set; }


        /// <summary>
        /// Esta info es extra. solo para detectar que error 
        /// </summary>
        public string api_errorMessage { get; set; }
        public DateTime api_enqueuedTime { get; set; }
        public string api_userPhopne { get; set; }
        public string api_accountPhopne { get; set; }

    }

    #endregion

    //Mensaje plantilla (HSM):
    public class messageHSM
    {
        public string messagetype { get; set; }

        /// <summary>
        /// phones: ["Pais+NºTeléfono"]
        /// </summary>
        public string[] phones { get; set; }
        /// <summary>
        ///  Son los parámetros que cada plantilla necesita para enviar el mensaje. Cada plantilla tiene definido su conjunto de parámetros.
        /// </summary>
        //[JsonProperty("params")]
        public string[] @params { get; set; }

        /// <summary>
        /// Es la plantilla que se va a utilizar
        /// TEXT, TEMPLATE etc
        /// </summary>
        public string template { get; set; }

        /// <summary>
        /// Es el identificador de la plantilla
        /// </summary>
        public string nameSpace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Media, el objeto multimedia a enviar en base64
        /// </summary>
        public string media { get; set; }

        /// <summary>
        /// Tipo de archivo a enviar
        /// </summary>
        public string mediaType { get; set; }

        public int PublicationId { set; get; } //Para el log del JSON enviado para el HSM.
    }

    public class messageMultimedia
    {
        /// <summary>
        /// MEDIA
        /// </summary>
        public string messagetype { get; set; }

        /// <summary>
        /// phones: Pais+NºTeléfono
        /// </summary>
        public string[] phones { get; set; }

        /// <summary>
        /// url o base64
        /// </summary>
        public string media { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mediatype { get; set; }

        //public string redirected { get; set; }

        public string text { get; set; }

        /// <summary>
        ///  Son los parámetros que cada plantilla necesita para enviar el mensaje. Cada plantilla tiene definido su conjunto de parámetros.
        /// </summary>
        //[JsonProperty("params")]
        //public string[] @params { get; set; }

        /// <summary>
        /// Esta info es extra. solo para detectar que error 
        /// </summary>
        //public String api_errorMessage { get; set; }
        //public System.DateTime api_enqueuedTime { get; set; }
    }


    /// <summary>
    /// Esta clase se construye solo para ser almacenada en el Isolation stotage es por eso q se la marca como Serializable
    /// Es un mapeo directo con messageRecivedList
    /// </summary>
    [Serializable]
    public class messageRecivedBEList : List<messageRecivedBE>
    {


        /// <summary>
        /// se retorna este casteo por que la clase messageRecivedList essta maracada como serializable y el json retornado no es el que espera el controller angular
        /// </summary>
        /// <returns></returns>
        public List<messageRecived> Get_messageRecived()
        {

            List<messageRecived> list = new List<messageRecived>();
            messageRecived i;
            ForEach(p =>
            {
                i = new messageRecived();

                i.externalId = p.externalId;
                i.deliveredDate = p.deliveredDate;
                i.readDate = p.readDate;
                i.messageorigin = p.messageorigin;
                i.messagetype = p.messagetype;
                i.text = p.text;
                i.template = p.template;
                i.@namespace = p.@namespace;
                i.@params = p.@params;

                i.api_accountPhopne = p.api_accountPhopne;
                i.api_enqueuedTime = p.api_enqueuedTime;
                i.api_errorMessage = p.api_errorMessage;
                i.api_userPhopne = p.api_userPhopne;


                list.Add(i);

            });

            return list;
        }
    }

    /// Esta clase se construye solo para ser almacenada en el Isolation stotage es por eso q se la marca como Serializable
    /// Es un mapeo directo con messageRecived
    /// </summary>
    [Serializable]
    public class messageRecivedBE
    {
        public messageRecivedBE()
        { }
        public messageRecivedBE(messageRecived messageRecived)
        {
            externalId = messageRecived.externalId;
            deliveredDate = messageRecived.deliveredDate;
            fechaCreacion = messageRecived.fechaCreacion;
            readDate = messageRecived.readDate;
            messageorigin = messageRecived.messageorigin;
            messagetype = messageRecived.messagetype;
            text = messageRecived.text;
            template = messageRecived.template;
            @namespace = messageRecived.@namespace;

            @params = messageRecived.@params;



        }
        /// <summary>
        /// Overload equal operator
        /// Framework BE = Edm Entity Model 
        /// </summary>
        /// <param name="pAngelPatients">Edm Model BE</param>
        public static explicit operator messageRecivedBE(messageRecived messageRecived)
        {
            return new messageRecivedBE(messageRecived);
        }

        /// <summary>
        /// Es el identificador externo del proveedor.
        /// </summary>
        public string externalId { get; set; }

        /// <summary>
        /// Es la fecha en la que se ha enviado el mensaje.
        /// </summary>
        public long? deliveredDate { get; set; }

        public long? fechaCreacion { get; set; }

        /// <summary>
        /// Es la fecha en la que se ha leído el mensaje
        /// </summary>
        public long? readDate { get; set; }
        /// <summary>
        /// Identifica si el mensaje ha sido enviado por una cuenta (Account), como por ejemplo KCRM, o bien ha sido el usuario (USER).
        /// </summary>
        public string messageorigin { get; set; }

        /// <summary>
        /// Identifica el estado en el que se encuentra el mensaje.
        /// </summary>
        public string messagestatus { get; set; }

        /// <summary>
        /// tipo de mensaje
        /// </summary>
        public string messagetype { get; set; }

        /// <summary>
        /// Identifica si el mensaje ha sido reenviado o no
        /// </summary>
        public bool redirected { get; set; }

        /// <summary>
        /// Contiene el cuerpo del mensaje. 
        /// </summary>
        public string text { get; set; }

        public string template { get; set; }
        //Es el identificador de la plantilla, para el ejemplo: "1e1a3508_c587_e845_d431_b6764aae2e52"
        public string @namespace { get; set; }
        public string[] @params { get; set; }


        /// <summary>
        /// Esta info es extra. solo para detectar que error 
        /// </summary>
        public string api_errorMessage { get; set; }

        /// <summary>
        /// Fecha hora en la q se encolo
        /// </summary>
        public DateTime api_enqueuedTime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string api_userPhopne { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string api_accountPhopne { get; set; }
    }


    public class findByPhoneResponse
    {

        public HttpStatusCode statusVal
        {
            get { return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), status); }

        }

        public string status { get; set; }

        public List<messageRecived> data { get; set; }

        public string errorData { get; set; }
    }

    public class WhatsAppTransferConversation
    {
        [JsonProperty("ownerAccount")]
        public string OwnerAccount { get; set; }

        [JsonProperty("clientNumber")]
        public string ClientNumber { get; set; }

        [JsonProperty("finalAccount")]
        public string FinalAccount { get; set; }
    }

    public class ApiConnect
    {
        public string WCURLApiPublicacion { get; set; }
        public string WCUserName { get; set; }
        public string WCPassword { get; set; }
        public bool ProxyEnabled { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyHost { get; set; }
        public string ProxyPassword { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyDomain { get; set; }
    }
}
