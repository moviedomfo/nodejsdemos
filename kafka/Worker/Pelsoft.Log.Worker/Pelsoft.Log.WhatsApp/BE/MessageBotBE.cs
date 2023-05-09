

namespace Pelsoft.Log.WhatsApp.BE
{


    public class MessageBotList : List<MessageBotBE>
    { }


    public class MessageBotBE
    {

        public int WMessageBotId { get; set; }
        public string WMessageBotMessage { get; set; }

        /// <summary>
        /// identificador del usuario que envió el mensaje, proveniente de Whatsapp. Es el número de teléfono.
        /// </summary>
        public string WMessageBotPhone { get; set; }

        /// <summary>
        /// identificador único del mensaje,  proveniente de Whatsapp. 
        /// </summary>
        public string WMessageBotExternalId { get; set; }
        public string WMessageBotAttachmentUrl { get; set; }
        public string WMessageBotTypeAttachment { get; set; }
        public DateTime? WMessageBotProcessDate { get; set; }

        /// <summary>
        /// identificador de la cuenta de Whatsapp del cliente, proveniente de Whatsapp.
        /// Este numero se obtiene de la configuracion del hook
        /// puede ser definido como parametro estatico para determinar el numero
        /// </summary>
        public string WWhatsAppId { get; set; }

        /// <summary>
        /// identificador del proveedor (terceros/brokers) a través del cual se obtuvo el mensaje.
        /// </summary>
        public string WProviderId { get; set; }
        public DateTime WMessageBotCreatedRow { get; set; }
        /// <summary>
        /// [NULL]: fecha/hora de envío del mensaje.
        /// </summary>
        public long? WMessageBotDeliveredDate { get; set; }
        public long? WMessageBotReadDate { get; set; }
        /// <summary>
        /// origen del mensaje. Puede tener los siguientes valores
        ///  ACCOUNT: proviene de la cuenta del cliente
        ///  USER: proviene de un usuario final
        /// </summary>
        public string WMessageBotOrigin { get; set; }
        public string WMessageBotStatus { get; set; }
        public string WMessageBotType { get; set; }
        public string WMessageBotParam { get; set; }
        public string WMessageBotTemplate { get; set; }
        public string WMessageBotNameSpace { get; set; }
        public bool WMessageBotRedirected { get; set; }
        public long? WMessageBotCreatedDate { get; set; }


        /// <summary>
        /// Esta info es extra. solo para detectar que error 
        /// </summary>
        public string api_errorMessage { get; set; }

        public DateTime api_enqueuedTime { get; set; }
    }


    public class MessageBotBEList_json : List<MessageBotBE_json>
    {

        public MessageBotBEList_json(MessageBotList messageBotList)
        {
            if (messageBotList.Count != 0)
            {
                MessageBotBE_json jsonBE = null;
                messageBotList.ToList().ForEach(item =>
                {
                    jsonBE = (MessageBotBE_json)item;
                    Add(jsonBE);
                });

            }
        }

    }

    public class MessageBotBE_json
    {

        public MessageBotBE_json() { }

        public MessageBotBE_json(MessageBotBE item)
        {


            api_errorMessage = item.api_errorMessage;
            WMessageBotAttachmentUrl = item.WMessageBotAttachmentUrl;
            WMessageBotCreatedDate = item.WMessageBotCreatedDate;
            WMessageBotCreatedRow = item.WMessageBotCreatedRow;
            WMessageBotDeliveredDate = item.WMessageBotDeliveredDate;
            WMessageBotExternalId = item.WMessageBotExternalId;

            WMessageBotId = item.WMessageBotId;
            WMessageBotMessage = item.WMessageBotMessage;
            WMessageBotNameSpace = item.WMessageBotNameSpace;
            WMessageBotOrigin = item.WMessageBotOrigin;
            WMessageBotParam = item.WMessageBotParam;

            WMessageBotPhone = item.WMessageBotPhone;
            WMessageBotProcessDate = item.WMessageBotProcessDate;
            WMessageBotReadDate = item.WMessageBotReadDate;
            WMessageBotRedirected = item.WMessageBotRedirected;
            WMessageBotStatus = item.WMessageBotStatus;

            WMessageBotTemplate = item.WMessageBotTemplate;
            WMessageBotType = item.WMessageBotType;
            WMessageBotStatus = item.WMessageBotStatus;
            WMessageBotTypeAttachment = item.WMessageBotTypeAttachment;

            WProviderId = item.WProviderId;
            WWhatsAppId = item.WWhatsAppId;

            api_enqueuedTime = item.api_enqueuedTime;
            api_errorMessage = item.api_errorMessage;
        }

        /// <summary>
        /// Overload equal operator
        /// Framework BE = Edm Entity Model 
        /// </summary>
        /// <param name="pAngelPatients">Edm Model BE</param>
        public static explicit operator MessageBotBE_json(MessageBotBE messageBotBE)
        {
            return new MessageBotBE_json(messageBotBE);
        }



        public int WMessageBotId { get; set; }
        public string WMessageBotMessage { get; set; }
        public string WMessageBotPhone { get; set; }

        /// <summary>
        /// identificador único del mensaje,  proveniente de Whatsapp. 
        /// </summary>
        public string WMessageBotExternalId { get; set; }
        public string WMessageBotAttachmentUrl { get; set; }
        public string WMessageBotTypeAttachment { get; set; }
        public DateTime? WMessageBotProcessDate { get; set; }

        /// <summary>
        /// identificador de la cuenta de Whatsapp del cliente, proveniente de Whatsapp.
        /// </summary>
        public string WWhatsAppId { get; set; }

        /// <summary>
        /// identificador del proveedor (terceros/brokers) a través del cual se obtuvo el mensaje.
        /// </summary>
        public string WProviderId { get; set; }
        public DateTime WMessageBotCreatedRow { get; set; }
        /// <summary>
        /// [NULL]: fecha/hora de envío del mensaje.
        /// </summary>
        public long? WMessageBotDeliveredDate { get; set; }
        public long? WMessageBotReadDate { get; set; }
        /// <summary>
        /// origen del mensaje. Puede tener los siguientes valores
        ///  ACCOUNT: proviene de la cuenta del cliente
        ///  USER: proviene de un usuario final
        /// </summary>
        public string WMessageBotOrigin { get; set; }
        public string WMessageBotStatus { get; set; }
        public string WMessageBotType { get; set; }
        public string WMessageBotParam { get; set; }
        public string WMessageBotTemplate { get; set; }
        public string WMessageBotNameSpace { get; set; }
        public bool WMessageBotRedirected { get; set; }
        public long? WMessageBotCreatedDate { get; set; }


        /// <summary>
        /// Esta info es extra. solo para detectar que error 
        /// </summary>
        public string api_errorMessage { get; set; }
        public DateTime api_enqueuedTime { get; set; }

    }


}




