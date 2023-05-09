
using Pelsoft.Log.Common;
using Pelsoft.Log.Common.ProcessBases;
using Pelsoft.Log.Common.Workers;
using System.Linq;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pelsoft.Log.Common.Services;


namespace WorkerServiceKon.workers
{
    /// <summary>
    /// This worker has the responsibility to listen all topic/queue  Products and persons
    /// </summary>
    public class ComerceWorker : BackgroundService 
    {
        //private readonly int commitPeriod = 1;
        Timer _Timer;


        //chanel/AccountDetailUnique  : WHATSAPP-
        //const string TOPICS = "customers,providers";
        //const string GROUP_ID = "rrhh-site01";
        //private readonly IConfiguration _Configuration;
        private readonly IPelsoftLogService _LogService;
        private readonly IServiceFactory factory;
        //private WorkerConfig _workerConfig;

        //public PersonWorker(IProcessor<PersonBE> processor, ILogger<PersonWorker> logger, IOptions<WorkerConfig> workerConfig)


        /// <summary>
        /// Worker constructor : here it will take place the service injection
        /// </summary>
        /// <param name="logService">Bussinnes Pelsoft logger</param>
        /// <param name="workerConfig">Bussinnes Pelsoft Worker config</param>
        /// <param name="configuration">Standar host configuration </param>
        /// <param name="commonWorkersService">Common component for all running workers</param>
        public ComerceWorker( IPelsoftLogService logService,IOptions<WorkerConfig> workerConfig,IConfiguration configuration, CommonWorkers commonWorkersService)
        {
            //_workerConfig = workerConfig.Value;
            //_Configuration = configuration;
            _LogService = logService;

            //there's no need to make dependency injection for test LogServiceFactoryTest          
            factory = new LogServiceFactoryTest(logService, configuration, commonWorkersService );

            //commitPeriod = (int)TimeSpan.FromSeconds(_workerConfig.KafkaConfig.CommitPeriod).TotalMilliseconds;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

       
            factory.LoadProcessFactory();
            List<Task> TaskList = new List<Task>();
            foreach (KeyValuePair<Guid, IProcessor> entry in factory.ProcessorDictionary)
            {
                entry.Value.IncidentEvent += porcess_IncidentEvent;
             
                entry.Value.Start();
            
            }
           

        }



        /// <summary>
        /// It doesn�t store the Log in the database, it only shows in the console the events that have occurred in some process .-
        /// 
        /// </summary>
        /// <param name="procId"></param>
        /// <param name="source"></param>
        /// <param name="incidentType"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        private void porcess_IncidentEvent(Guid? procId, string source, PelsoftEnums.IncidentType incidentType, string message, Exception ex)
        {


            if (incidentType == PelsoftEnums.IncidentType.FatalError)
            {
                if (procId.HasValue)
                {
                    factory.ProcessorDictionary[procId.Value].Stop();
                }
                    
            }


            if (incidentType == PelsoftEnums.IncidentType.Error || incidentType == PelsoftEnums.IncidentType.FatalError)
            {
                if (ex == null)
                    ex = new Exception(message);
                _LogService.Logger_logError(ex , incidentType);
                

            }

            if (incidentType != PelsoftEnums.IncidentType.Error)
            {
                string? typeNAme = Enum.GetName(typeof(PelsoftEnums.IncidentType), incidentType);
                _LogService.Logger_logInfo(typeNAme + " -> source " + source + message);
            }
                


        }


        /// <summary>
        /// Calls ExecuteAsync 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            
            //_Timer = new Timer(timmerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _Timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void timmerElapsed(object sender)
        {
            //string log = string.Format("Timmer runing {0} {0}", DateTimeOffset.Now.ToString(), "Worker engine ");
            //logInfo(log);

            ////_logger.LogError("Error sample");

            //try
            //{
            //    Console.WriteLine(log);
            //}
            //catch (Exception ex)
            //{
            //    logError(PelsoftEnums.IncidentType.Error, ex);
            //}

        }
    
     

     
    }
}