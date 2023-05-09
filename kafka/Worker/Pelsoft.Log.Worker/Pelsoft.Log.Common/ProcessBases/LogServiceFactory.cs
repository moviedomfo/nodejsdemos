using Pelsoft.Log.Common;
using Pelsoft.Log.Common.DAC;
using Pelsoft.Log.Common.ProcessBases;
using Pelsoft.Log.Common.Workers;
using Fwk.HelperFunctions;
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
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public  Dictionary<Guid, IProcessor> ProcessorDictionary { get; set; }



        /// <summary>
        /// 
        /// </summary>
        static List<ChannelAssembly> ChannelAssemblyList;


        /// <summary>
        /// Statick constructor tht call Initialize
        /// </summary>
        public ServiceFactory()
        {
            Initialize();
        }


        /// <summary>
        /// Load ChannelAssemblyList from database
        /// </summary>
        public void Initialize()
        {
            PelsoftException te;


            ChannelAssemblyList = LogServiceDAC.RetriveAllChannelAssembly(CommonWorkers.Instance.ServiceInstanceUnique);
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
            var accounts = LogServiceDAC.Retrive_All_AccountsInstances(CommonWorkers.Instance.ServiceInstanceUnique);



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

                    processor = (IProcessor)ReflectionFunctions.CreateInstance(item.AssemblyStrongName);// new Object[] { account });
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
    }
}
