using Pelsoft.Log.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Pelsoft.Log.Comerce.BE;
using Pelsoft.Log.Comerce.Repos;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.ProcessBases;
using Microsoft.Extensions.Configuration;

namespace Pelsoft.Log.Comerce
{
    public class PersonsProcessor : ProcessorBase 
    {
    
        private readonly IPersonRepository personRepository;

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logService"></param>
        /// <param name="configuration"></param>
        /// <param name="accountInfo"></param>
        public PersonsProcessor( IPelsoftLogService logService, IConfiguration configuration, AccountsInstancesServicesBE accountInfo)  :base( logService,accountInfo)          
        {
            /// TODO:  Diseño: si esta impleentecion es por refleccion conviene crear directamente la dependencia 
            /// Si cada servicio sabe espesificamente que Dll o Iprocessor usara (no reflection) lo que conviene hacer es usar dependenci Injection pero para esto 
            /// hay q agregarlos ene el buil del servicio AddSingleton
            personRepository = new PersonRepository (configuration);

            //TODO: kafka BootstrapServers -> analizar si esta configuracion viene configurada o se espesifica comun para todos los procesos
            this.BootstrapServers = "localhost:9092";

            //TODO: kafka Topics -> analizar si esta configuracion viene configurada desde la BD
            this.Topics = "customers,providers";
            //TODO: kafka GroupId -> analizar se configura en appSettings o tomaria el nombre del mismo servicio
            this.GroupId = "desarrollo-site01";

        }


        /// <summary>
        /// Store in Person table any person or spesific customer
        /// </summary>
        /// <param name="message">Menssage or value from queue</param>
        /// <param name="topic"> can be persons or customer </param>
        /// <returns></returns>
        public override async Task DoWork(string jsonMessage, string topic )
        {
            
            var message = TrySerialize<PersonBE>(jsonMessage);
            if(message != null)
            {
                message.kafka_Topic = topic;
                await CreatePerson(message);
            }
            
        }

        /// <summary>
        /// Verifica cambios en config y proxy
        /// </summary>
        public override void RefreshProcessData()
        {

      
        }

  

        

        /// <summary>
        /// 
        /// </summary>
        public async Task CreatePerson(PersonBE person)
        {


            string _Response = string.Empty;
            string _Error = string.Empty;
            string _Status = string.Concat("Inicio create_persons: " + DateTime.Now.ToString("HH:mm"));

            person.kafka_Topic = person.kafka_Topic is null ? "sintopic": person.kafka_Topic  ;
            person.DocNumber  = person.DocNumber is null ? "00000000" : person.DocNumber;
            personRepository.Insert(person);

            _Status = string.Concat(_Status, ";Fin crearte_person: " + DateTime.Now.ToString("HH:mm"));

            //Inserto el status.
            ProcessDAC.CreateStatus(AccountInfo.ConnectionString, this.currentProcessDetailsID, _Status, Convert.ToInt32(AccountInfo.ChannelType), _Response, _Error);
        }

        

        
    }
}
