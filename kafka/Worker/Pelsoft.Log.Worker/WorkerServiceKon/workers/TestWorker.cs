
using Pelsoft.Log.Common;
using Pelsoft.Log.Common.Workers;
using Microsoft.Extensions.Options;
using Pelsoft.Log.Common.Services;
using System.Text;

namespace WorkerServiceKon.workers
{
    /// <summary>
    /// This worker is only for simple an fast setting test
    /// </summary>
    public class TestWorker : BackgroundService 
    {
    
        private readonly IPelsoftLogService _LogService;
        private readonly IServiceFactory factory;
        private WorkerConfig _workerConfig;


        /// <summary>
        /// TestWorker constructor : here it will take place the service injection
        /// </summary>
        /// <param name="logService">Bussinnes Pelsoft logger</param>
        /// <param name="workerConfig">Bussinnes Pelsoft Worker config</param>
        /// <param name="configuration">Standar host configuration </param>
        /// <param name="commonWorkersService">Common component for all running workers</param>
        public TestWorker( IPelsoftLogService logService,IOptions<WorkerConfig> workerConfig,IConfiguration configuration, CommonWorkers commonWorkersService)
        {
    
            _LogService = logService;
            _workerConfig = workerConfig.Value;


            if (_workerConfig == null)
            {
                _LogService.Logger_logError(new Exception("WorkerConfig is NULL in constructor"));
                return;
            }

            if (_workerConfig.KafkaConfig == null)
            {
                _LogService.Logger_logError(new Exception("KafkaConfig in WorkerConfig settings is NULL in constructor"));
                return;
            }



            _LogService.Logger_logInfo("TestWorker constructor executed");



        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Do your preparation (e.g. Start code) here
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoSomethingAsync();
            }


        }

        Task DoSomethingAsync()
        {
            return Task.Run(() =>
            {
                string log = string.Format("{0}", "Worker engine DoSomething");
                StringBuilder str = new StringBuilder();

                str.AppendLine(log);
                if (_workerConfig == null)
                {
                    _LogService.Logger_logError(new Exception("WorkerConfig is NULL"));
                    System.Threading.Thread.Sleep(5000);
                    return;
                }

                str.AppendLine("Kafka GROUP_ID : " + _workerConfig.KafkaConfig.GROUP_ID);
                str.AppendLine("ServiceName : " + _workerConfig.ServiceName);
                _LogService.Logger_logInfo(str.ToString());
                System.Threading.Thread.Sleep(5000);

            });
        }

        /// <summary>
        /// Calls ExecuteAsync 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            

            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

    
     

     
    }
}