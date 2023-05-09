
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppMessageBotBE 
    {
        public int WMessageBotId { get; set; }
        public string WMessageBotMessage { get; set; }
        public string WMessageBotPhone { get; set; }
        public string WMessageBotExternalId { get; set; }
        public string WMessageBotAttachment { get; set; }
        public string WMessageBotMediaType { get; set; }
        public DateTime? WMessageBotProcessDate { get; set; }
        public string WWhatsAppId { get; set; }
        public string WProviderId { get; set; }
        public DateTime WMessageBotCreatedRow { get; set; }
        public string WMessageBotOrigin { get; set; }
        public string WMessageBotStatus { get; set; }
        public string WMessageBotType { get; set; }
        public string WMessageBotParam { get; set; }
        public string WMessageBotTemplate { get; set; }
        public string WMessageBotNameSpace { get; set; }
        public bool WMessageBotRedirected { get; set; }
        public long? WMessageBotDeliveredDateEpoch { get; set; }
        public DateTime? WMessageBotDeliveredDate { get; set; }
        public long? WMessageBotReadDateEpoch { get; set; }
        public DateTime? WMessageBotReadDate { get; set; }
        public long WMessageBotCreatedDateEpoch { get; set; }
        public DateTime WMessageBotCreatedDate { get; set; }
        public string WMessageBotFileName { get; set; }
    }

    public class WhatsAppMessageBotBEList : List<WhatsAppMessageBotBE>
    {
    }
}
