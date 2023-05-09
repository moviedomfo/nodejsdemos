using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common
{
    public class Proxy
    {
		public int ProxyId { get; set; }

		public int ProxyPort { get; set; }

		public string ProxyHost { get; set; }

		public string ProxyUserName { get; set; }

		public string ProxyPassword { get; set; }	

		public string ProxyDomain { get; set; }

		public bool ProxyActiveFlag { get; set; }

		public System.DateTime ProxyCreatedRow { get; set; }
	}
}
