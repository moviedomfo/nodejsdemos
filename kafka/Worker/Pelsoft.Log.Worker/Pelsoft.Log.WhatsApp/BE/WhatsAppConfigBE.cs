
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppConfigBE 
    {
        public int WCId { get; set; }
        public DateTime WCCreated { get; set; }
        public bool WCActiveFlag { get; set; }
        public int WCQuantityToPublish { get; set; }
        public int WCRetriesQuantity { get; set; }
        public int? WCLimit { get; set; }
        public int WCQuantityToPublishOnDemand { get; set; }
        public DateTime WCStartDate { get; set; }
        public DateTime? WCEndDate { get; set; }
        public int WCQuantityConvertToCase { get; set; }
        public int WCQuantityAssignAttentionQueue { get; set; }
        public string WCPassword { get; set; }
        public string WCUserName { get; set; }
        public string WCURLApiPublicacion { get; set; }
        public int WAccountId { get; set; }
        public DateTime WCModifiedDate { get; set; }
        public int WCModifiedByUser { get; set; }
        public string WAccountPhone { get; set; }
        public Guid AccountDetailUnique { get; set; }
        public int WCMessageQuantityLog { get; set; }
        public int WCRetriesQuantityLog { get; set; }
        public string WCDestAccount { get; set; }
        public bool WCShortenUrl { get; set; }
    }

    public class WhatsAppConfigBEList : List<WhatsAppConfigBE>
    {
    }
}
