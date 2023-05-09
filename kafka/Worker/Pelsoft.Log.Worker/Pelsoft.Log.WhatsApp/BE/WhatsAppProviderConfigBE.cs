using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pelsoft.Log.WhatsApp.BE
{
 
    public class WhatsAppProviderConfigBE 
    {
        public int PCId { get; set; }
        public string PCProviderId { get; set; }
        public int PCWCId { get; set; }
        public bool PCMessageBotReadDateUTC { get; set; }
        public bool PCMessageBotCreatedDateUTC { get; set; }
        public bool PCMessageBotDeliveredDateUTC { get; set; }
        public DateTime PCCreatedDate { get; set; }
        public DateTime PCModifiedDate { get; set; }
        public int PCModifiedByUser { get; set; }
    }
}
