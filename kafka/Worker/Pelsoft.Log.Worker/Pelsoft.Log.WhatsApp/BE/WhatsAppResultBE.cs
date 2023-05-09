using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppResultBE
    {
        public int status { get; set; }
        public WhatsAppDataBE[] data { get; set; }
        public WhatsAppErrorData errorData { get; set; }
        public object aditional { get; set; }
    }

    public class WhatsAppDataBE
    {
        public long fechaCreacion { get; set; }
        public string externalId { get; set; }
        public object deliveredDate { get; set; }
        public object readDate { get; set; }
        public string messageorigin { get; set; }
        public string messagestatus { get; set; }
        public string messagetype { get; set; }
        public bool redirected { get; set; }
        public object hashCode { get; set; }
        public string text { get; set; }
    }

    public class WhatsAppErrorData
    {
        public string message { get; set; }
        public string cause { get; set; }
    }

}
