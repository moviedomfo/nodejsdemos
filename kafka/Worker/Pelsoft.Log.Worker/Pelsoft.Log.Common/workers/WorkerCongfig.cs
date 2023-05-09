using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common.Workers
{
    /// <summary>
    /// This config class contains all possible worker-service settings. Such as InstanceId, kafka configs , paths an so on.-
    /// </summary>
    public class WorkerConfig
    {


        /// <summary>
        /// Name of the Pelsoft Master cnnstring .- 
        /// </summary>
        public string CnnstringNameMaster { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Hosted Service Isntance, this guid is configurated in datatabe. 
        /// To get instance info use LogServiceDAC.GetSvcLogInstance (isntanceGuid) 
        /// One to one with int ServiceInstanceId 
        /// </summary>
        public string ServiceInstanceId { get; set; }

        /// <summary>
        /// kafka confisa
        /// </summary>
        public Kafka KafkaConfig { get; set; }

        /// <summary>
        /// Flag to indicate if logs will be made, (audit and error logs)
        /// </summary>
        public bool PerformLog { get; set; }

    }

    /// <summary>
    /// Kafka settings for consumer.-
    /// Maybe, in future, this config  should be for each process
    /// </summary>
    public class Kafka
    {
        /// <summary>
        ///  is a comma-separated list of host and port pairs that are the addresses of the Kafka brokers in a 
        ///  "bootstrap" Kafka cluster that a Kafka client connects to initially to bootstrap itself. 
        ///  A Kafka cluster is made up of multiple Kafka Brokers
        /// </summary>
        public string BootstrapServers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CommitPeriod { get; set; }

        /// <summary>
        /// Is a coma-separated list of topics that consumer is litening
        /// this config  was set for each process
        /// </summary>
        //public string Topics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GROUP_ID { get; set; }

    }
}
