using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Pelsoft.Log.Comerce.BE;

namespace Pelsoft.Log.Comerce.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private IConfiguration Configuration;
        public PersonRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Insert(PersonBE person)
        {
            try
            {
                string connString = Configuration.GetConnectionString("farma");


                using (SqlConnection cnn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Persons_i]", cnn) { CommandType = CommandType.StoredProcedure })
                    {
                        cnn.Open();


                        cmd.Parameters.AddWithValue("Name", person.FirstName.Trim());
                        cmd.Parameters.AddWithValue("Lastname", person.Lastname.Trim());


                        cmd.Parameters.AddWithValue("CLoudId", person.Id);


                        if (!string.IsNullOrEmpty(person.DocNumber))
                            cmd.Parameters.AddWithValue("DocNumber", person.DocNumber.Trim());

                        if (!string.IsNullOrEmpty(person.Phone))
                            cmd.Parameters.AddWithValue("Phone", person.Phone.Trim());



                        if (!string.IsNullOrEmpty(person.City))
                            cmd.Parameters.AddWithValue("City", person.City.Trim());

                        if (person.GeneratedDate.HasValue)
                            cmd.Parameters.AddWithValue("GeneratedDate", person.GeneratedDate);
                        
                        cmd.Parameters.AddWithValue("@kafka_Topic", person.kafka_Topic);

                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersonBE? TrySerialize(string value)
        {
            try
            {
                var person = JsonConvert.DeserializeObject<PersonBE>(value);

                return person;

            }
            catch
            {
                return null;
            }


        }
    }


    public interface IPersonRepository
    {
        void Insert(PersonBE person);
        PersonBE? TrySerialize(string value);
    }
}
