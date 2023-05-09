
using Pelsoft.Log.Common.ProcessBases;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.Workers;
using Fwk.HelperFunctions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class LogServiceFactoryTest : IServiceFactory
    {

        private readonly IPelsoftLogService _LogService;
        private readonly IConfiguration _Configuration;
        //private readonly CommonWorkers _CommonWorkers;
        /// <summary>
        /// 
        /// </summary>
        public  Dictionary<Guid, IProcessor> ProcessorDictionary { get; set; }



        /// <summary>
        /// 
        /// </summary>
        static List<ChannelAssembly> ChannelAssemblyList;


        /// <summary>
        /// Static constructor tht call Initialize
        /// </summary>
        public LogServiceFactoryTest(IPelsoftLogService logService, IConfiguration configuration, CommonWorkers commonWorkersService)
        {
            _LogService = logService;
            _Configuration = configuration;
            Initialize();
        }


        /// <summary>
        /// Load ChannelAssemblyList from database
        /// </summary>
         void Initialize()
        {
            PelsoftException te;

            
            ChannelAssemblyList = RetriveAllChannelAssembly(CommonWorkers.Instance.ServiceInstanceUnique);
            if (ChannelAssemblyList == null)
            {
                te = new PelsoftException(" No existen cargados ChannelAssembly para Instance Guid " + CommonWorkers.Instance.ServiceInstanceUnique, null);
                te.ErrorCode = 1312;
                throw te;
            }


        }

        /// <summary>
        /// Perform service initialization (by now create dynamically process instance using flection.
        /// Do not start the process, only generete it instance an append to the dictionary ProcessorDictionary.-
        /// This method brings together the capabilities of the Pelsoft Engine.Start() and LogServiceFactory.ProcessFactory
        /// </summary>
        public void LoadProcessFactory()
        {
            ProcessorDictionary = new Dictionary<Guid, IProcessor>();

            var accounts = Retrive_All_AccountsInstances(CommonWorkers.Instance.ServiceInstanceUnique);



            // TODO:  Tema de análisis y rediseño de instanciacion de IProcessor
            // Aqui por  cada account denbo crear una instancia de IProcessor ?
            // Seguiremos instaqnciando dinamicamente Assembly o estaran ya fijos los IProcessor por cada instancia y crearemos
            // un nuevo IProcessor que sera el Hilo para cada cuenta pero del mismo tipo IProcessor
            // Tiene sentido levantar hilos de otro canal?? en este BackgroundService
            // De ser necesario levantar diferentes IProcessor podria ser mejor (No usar Reflecion) y directamente inyectarlos en el Startup
            //_processor.Start(stoppingToken, accounts[0]);

            #region ProcessFactory
            accounts.ForEach(account =>
            {
                ChannelAssembly item = null;
                IProcessor processor = null;

                //Create IProcessor instance  by reflection
                try
                {
                    //Fetch assembly info

                    
                    item = ChannelAssemblyList.Where(p => p.ChannelType.Equals((int)account.ChannelType)).FirstOrDefault<ChannelAssembly>();

                  

                    processor = (IProcessor)ReflectionFunctions.CreateInstance(item.AssemblyStrongName, new Object[] {_LogService, _Configuration, account });
                    processor.ProcessorId = account.AsiDetailUnique;

                    if ((item.AssemblyStrongName.Contains("TwitterSearch")) && (item.ChannelType == (int)Pelsoft.Log.Common.PelsoftEnums.Channeltype.Tweeter))
                    {
                        item.ChannelType = (int)Pelsoft.Log.Common.PelsoftEnums.Channeltype.TwitterSearch;
                    }
                }
                catch (Exception ex)
                {
                    StringBuilder str = new StringBuilder("No se puede instanciar el proceso ");
                    str.AppendLine(account.ToString());
                    if (item != null)
                        str.AppendLine(String.Concat("AssemblySignature: ", item.AssemblyStrongName));
                    PelsoftException ep = new PelsoftException(str.ToString(), ex);
                    ep.ErrorCode = 1310;
                    throw ep;
                }

                if (processor == null)
                {
                    PelsoftException ep = new PelsoftException(String.Concat("No se puede inicializar el proceso de AccountsInstancesServices = ", account.ToString()), null);
                    ep.ErrorCode = 1310;
                    throw ep;
                }

                #region processor != null)

                ProcessorDictionary.Add(processor.ProcessorId, processor);

                #endregion
            });

            #endregion
        }


        List<ChannelAssembly> RetriveAllChannelAssembly(Guid instanceId)
        {

            List<ChannelAssembly> items = new List<ChannelAssembly>();
            
            ChannelAssembly item = new ChannelAssembly();
            item.ChannelType = (int)PelsoftEnums.Channeltype.Person;
            item.ActiveFlag = true;
            item.AssemblyStrongName = "Pelsoft.Log.Comerce.PersonsProcessor, Pelsoft.Log.Comerce";

            items.Add(item);


            item = new ChannelAssembly();
            item.ChannelType = (int)PelsoftEnums.Channeltype.Products;
            item.ActiveFlag = true;
            item.AssemblyStrongName = "Pelsoft.Log.Comerce.ProductProcessor, Pelsoft.Log.Comerce";
            items.Add(item);

            return items; 

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instancesServiceGuid"></param>
        /// <returns></returns>
        List<AccountsInstancesServicesBE> Retrive_All_AccountsInstances(Guid instancesServiceGuid)
        {

            List<AccountsInstancesServicesBE> list = new List<AccountsInstancesServicesBE>();
            AccountsInstancesServicesBE item = null;

            #region add account Product for CompanyA
            item = new AccountsInstancesServicesBE();

            item.AsiDetailUnique = Guid.NewGuid();
            item.AccountDetailUnique = Guid.NewGuid();
            item.AccountDetailDescrip = "Comerce Products";
            item.Interval = 10;
            item.AccountName = "Company A products";
            item.ServiceChannelName = "products";
       
            item.Proxy = null;

            item.ConnectionString = "";
            item.ChannelType = PelsoftEnums.Channeltype.Products;
            item.AuditMailSetting = new AuditMailSetting_Struct();
       

            item.OriginalTypeId = (int)PelsoftEnums.Channeltype.Products;
            item.OriginalTypeName = "Comerce porduct test";
            item.Topic = "products";
            list.Add(item);
            #endregion

            #region add account Person for CompanyA
            item = new AccountsInstancesServicesBE();

            item.AsiDetailUnique = Guid.NewGuid();
            item.AccountDetailUnique = Guid.NewGuid();
            item.AccountDetailDescrip = "Comerce Persons";
            item.Interval = 10;
            item.AccountName = "Company A Persons";
            item.ServiceChannelName = "Persons";

            item.Proxy = null;

            item.ConnectionString = "";
            item.ChannelType = PelsoftEnums.Channeltype.Person;
            item.AuditMailSetting = new AuditMailSetting_Struct();


            item.OriginalTypeId = (int)PelsoftEnums.Channeltype.Products;
            item.OriginalTypeName = "Comerce Persons original test";
            item.Topic = "customers,providers";
            list.Add(item);
            #endregion
            return list;

        }
    }
}
