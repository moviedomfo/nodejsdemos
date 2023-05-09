using WorkerServiceKon.workers;
using Pelsoft.Log.Comerce.Repos;
using Pelsoft.Log.Comerce;
using Pelsoft.Log.Common;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.Workers;
using Pelsoft.Log.Common.ProcessBases;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddCommonServices(context.Configuration);

        services.AddComerceServices(context.Configuration);
        
        //#region injections needed for all components 
        //services.AddSingleton<ICnnStringService, CnnStringService>();
        //services.AddSingleton<IPelsoftLogService, PelsoftLogService>();
        services.AddSingleton<CommonWorkers>();
        //#endregion


        /// Procces generator (by now from configured arssembly ->dinamically )
        services.AddSingleton<IServiceFactory, LogServiceFactoryTest>();

        //Esto seria necesario si no usamos reflection
        //services.AddSingleton<IProcessor, PersonsProcessor>();
        //services.AddSingleton<IProcessor, ProductProcessor>();


        #region workers 
        services.AddHostedService<ComerceWorker>();
        //services.AddHostedService<TestWorker>();
        //services.AddHostedService<PelsoftWorker>();
        #endregion

        services.AddOptions< WorkerConfig>();

        //var configurationRoot = context.Configuration;
        services.Configure<WorkerConfig>(context.Configuration.GetSection(nameof(WorkerConfig)));
        var workerConfig = context.Configuration.GetSection("WorkerConfig").Get<WorkerConfig>();

        


    }).Build();

//var CommonWorkers = (CommonWorkers) host.Services.GetService(typeof(CommonWorkers));
//   CommonWorkers.Initialize();


    


await host.RunAsync();


