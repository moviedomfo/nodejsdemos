
using Confluent.Kafka;
using Pelsoft.Log.Common;
using Pelsoft.Log.Common.DAC;
using Pelsoft.Log.Common.ProcessBases;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.Workers;
using Pelsoft.Log.WhatsApp.BE;
using Fwk.Exceptions;
using Microsoft.Extensions.Options;


namespace WorkerServiceKon.workers
{
    /// <summary>
    /// Este worker tiene la responsabilidad unica instanciar los procesos (hilos) acorde su configuracion en BD 
    /// La configuracion se obtiene a partir de su InstanceGuid
    /// de escuchar la/s cola/s TOPICS
    /// </summary>
    public class PelsoftWorker : BackgroundService
    {
        public static string InstanceGuid = string.Empty;





        //chanel/AccountDetailUnique  : WHATSAPP-

        //Timer _Timer;
        //private readonly IProcessor _processor;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<PelsoftWorker> _logger;
        
        /// <summary>
        /// This config is preconfigured and in runtime loaded from appsetting file. 
        /// 
        /// </summary>
        //private readonly WorkerConfig _workerConfig;

        /// <summary>
        /// Config (key-value) del de appsetting
        /// </summary>
        //private readonly IConfiguration _Configuration;

        /// <summary>
        /// Pelsoft logger that impements and represents a custom event log for epiron logs 
        /// </summary>
        private readonly IPelsoftLogService _LogService;

        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceFactory factory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">Injected by DI</param>
        /// <param name="workerConfig">Pelsoft log service config. Injected  by DI</param>
        public PelsoftWorker(IPelsoftLogService logService, IConfiguration configuration, CommonWorkers commonWorkersService)
        {
            //_workerConfig = workerConfig.Value;
            //_Configuration = configuration;
            factory = new LogServiceFactoryTest(logService, configuration, commonWorkersService);

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
                _LogService.Logger_logError(ex, incidentType);


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
            //_Timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

    
     


    }
}