using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pelsoft.Log.Common
{
    /// <summary>
    /// Estructura liviana de svc_Instance lin q
    /// </summary>
    public class ServiceInstance_Struct
    {

        public ServiceInstance_Struct()
        {
            Interval = 8;
        }
        public Int32 Interval { get; set; }
        public String LogFileFullName { get; set; }
        public Boolean LogOnFile { get; set; }
        public String Cnnstring_Master { get; set; }

        
        /// <summary>
        /// Hosted Serivice Isntance, this guid is configurated in datatabe. 
        /// To get instance info use LogServiceDAC.GetSvcLogInstance (isntanceGuid) 
        /// One to one with int ServiceInstanceId 
        /// </summary>
        public int InstancesServiceId { get; set; }

        /// <summary>
        /// One to one ServiceInstance (guid)
        /// </summary>
        public Guid ServiceInstanceUnique { get; set; }

        public string HostName { get; set; }

        public string InstanceName { get; set; }

        public  string ServiceName { get; set; }

        public string Ip { get; set; }
       
        
        public AuditMailSetting_Struct AuditMailSetting { get; set; }


    }
}
