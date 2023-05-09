
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppMessageBE 
    {
        public int WMId { get; set; }
        public int WFromUserId { get; set; }
        public int WCId { get; set; }
        public int ProcessDetailsId { get; set; }
        public string WMMessage { get; set; }
        public DateTime WMCreatedRow { get; set; }
        public DateTime? WMProcessStart { get; set; }
        public DateTime? WMProcessEnd { get; set; }
        public Guid WMGUID { get; set; }
        public string WMState { get; set; }
        public string WMType { get; set; }
        public DateTime? WMDeliveredDate { get; set; }
        public string WMExternalId { get; set; }
        public Guid AccountDetailUnique { get; set; }
        public int? ProcessDetailIdConversionToCase { get; set; }
        public DateTime? WMReadDate { get; set; }
        public DateTime WMCreatedDate { get; set; }
    }

    public class WhatsAppMessageBEList : List<WhatsAppMessageBE>
    {
    }
}
