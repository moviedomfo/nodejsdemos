using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pelsoft.Log.WhatsApp.BE
{
    public class WhatsAppTemplateAttributesBE
    {
        public string TemplateWhatsappId {set;get;}
        public string TemplateWhatsappName { set; get; }
        public int TAVId { set; get; }
        public string TAVValue { set; get; }
        public int TAOrder { set; get; }
        public bool TemplateWithoutAttribute { set; get; }

    }
    public class WhatsAppTemplateAttributesBEList :List<WhatsAppTemplateAttributesBE>
    {

    }
}
