using Pelsoft.Log.Common;
using Newtonsoft.Json;
using Pelsoft.Log.Comerce.BE;
using Pelsoft.Log.Comerce.Repos;

using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.ProcessBases;
using Microsoft.Extensions.Configuration;

namespace Pelsoft.Log.Comerce
{
    public class ProductProcessor : ProcessorBase 
    {
    
        private readonly IProducRepository producRepository;

      

        public ProductProcessor( IPelsoftLogService logService,IConfiguration configuration,AccountsInstancesServicesBE accountInfo) : base(logService, accountInfo)
        {
            /// TODO:  Diseño: si esta impleentecion es por refleccion conviene crear directamente la dependencia 
            /// Si cada servicio sabe espesificamente que Dll o Iprocessor usara (no reflection) lo que conviene hacer es usar dependenci Injection pero para esto 
            /// hay q agregarlos ene el buil del servicio AddSingleton
            producRepository = new ProductRepository(configuration);


            //TODO: kafka BootstrapServers -> analizar si esta configuracion viene configurada o se espesifica comun para todos los procesos
            this.BootstrapServers = "localhost:9092";

            //TODO: kafka Topics -> analizar si esta configuracion viene configurada desde la BD
            this.Topics = "products";
            //TODO: kafka GroupId -> analizar se configura en appSettings o tomaria el nombre del mismo servicio
            this.GroupId = "desarrollo-site01";
        }


        /// <summary>
        /// Store product
        /// </summary>
        /// <param name="message">Menssage or value from queue</param>
        /// <param name="topic"> can be products only </param>
        /// <returns></returns>
        public override async Task DoWork(string jsonMessage, string topic)
        {
          

            var product = base.TrySerialize<ProductBE>(jsonMessage);
            if (product != null)
            {
                product.kafka_Topic = topic;
                await Create(product);
            }

            
        }

        /// <summary>
        /// Verifica cambios en config y proxy
        /// </summary>
        public override void RefreshProcessData()
        {
            ////Busca la configuración.

        }

  

        

        /// <summary>
        /// 
        /// </summary>
        public async Task Create(ProductBE product)
        {


            string _Response = string.Empty;
            string _Error = string.Empty;
            string _Status = string.Concat("Inicio create_products " + DateTime.Now.ToString("HH:mm"));

            
            producRepository.Insert(product);

            _Status = string.Concat(_Status, ";Fin crearte_person: " + DateTime.Now.ToString("HH:mm"));

        }

        

        
    }
}
