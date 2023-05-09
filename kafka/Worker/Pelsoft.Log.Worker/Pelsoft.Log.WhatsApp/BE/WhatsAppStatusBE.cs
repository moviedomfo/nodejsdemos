using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppStatusBE
    {
        public int status { get; set; }
        public object data { get; set; }
        public Errordata[] errorData { get; set; }
        public object aditional { get; set; }
    }
    
    public class Errordata
    {
        public string message { get; set; }
        public string cause { get; set; }
    }

}
