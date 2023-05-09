using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppUserBE 
    {
        public int WUserId { get; set; }
        public string WUserPhone { get; set; }
        public DateTime WUserCreatedDate { get; set; }
        public bool WuserActiveFlag { get; set; }
        public string WUserName { get; set; }
        public byte[] WUserImage { get; set; }
        public DateTime? WUserModifiedDate { get; set; }
        public Guid WUserGUID { get; set; }
    }

    public class WhatsAppUserBEList : List<WhatsAppUserBE>
    {
    }
}
