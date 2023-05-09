
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppAccountBE 
    {
        public int WAccountId { get; set; }
        public string WAccountName { get; set; }
        public string WAccountPhone { get; set; }
        public DateTime WAccountCreatedDate { get; set; }
        public bool WAccountActiveFlag { get; set; }
        public int WAccountModifiedByUserId { get; set; }
        public Guid AccountDetailUnique { get; set; }
        public DateTime WAccountModifiedDate { get; set; }
    }

    public class WhatsAppAccountBEList : List<WhatsAppAccountBE>
    {
    }
}
