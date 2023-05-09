using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common
{
    public class ServiceInstance
    {
		public int ServiceInstanceId;

		public System.Guid ServiceInstanceGuid{ get; set; }

		public string SIHostName{ get; set; }

		public string SIIp{ get; set; }

		public string SIName{ get; set; }

		public bool SILogOnFile{ get; set; }

		public string SIMailRecipients{ get; set; }

		public int SIInterval{ get; set; }

		public bool SIActiveFlag{ get; set; }

		public System.DateTime SICreatedrow{ get; set; }

		public int MailSenderId { get; set; }

		public int PSIType { get; set; }

		public AuditMailSetting_Struct AuditMailSetting { get; set; }
	}
}
