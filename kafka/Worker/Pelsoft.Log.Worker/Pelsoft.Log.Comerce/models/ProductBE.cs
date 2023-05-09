using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Comerce.BE
{
    public class ProductBE
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Material { get; set; }
        public string Lab { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public DateTime? GeneratedDate { get; set; }

        public string kafka_Topic { get; set; }





    }
}
