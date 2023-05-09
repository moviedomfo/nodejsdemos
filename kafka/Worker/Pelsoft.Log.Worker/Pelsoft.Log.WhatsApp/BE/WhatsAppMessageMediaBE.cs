
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppMessageMediaBE 
    {
        public int WMMediaID { get; set; }
        public int WMId { get; set; }
        public string WMMediaType { get; set; }
        public string WMFileId { get; set; }
        public DateTime WMCreateRow { get; set; }
        public byte[] WMFileInBytes { get; set; }
        public string WMMimeType { get; set; }
        public int MediaTypeId { get; set; }
        public string WMName { get; set; }
        public string WMOriginalAttachment { get; set; }
    }

    public class WhatsAppMessageMediaBEList : List<WhatsAppMessageMediaBE>
    {
    }
}
