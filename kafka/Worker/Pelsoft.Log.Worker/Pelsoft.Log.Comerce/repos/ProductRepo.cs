using Pelsoft.Log.Comerce.BE;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Pelsoft.Log.Comerce.Repos
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductRepository : IProducRepository
    {
        private IConfiguration _Configuration;
 
        public ProductRepository(IConfiguration _configuration)
        {
            _Configuration = _configuration;
            
        }
        public void Insert(ProductBE product)
        {
            try
            {

                
                string connString = _Configuration.GetConnectionString("farma");


                using (SqlConnection cnn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Products_i]", cnn) { CommandType = CommandType.StoredProcedure })
                    {
                        cnn.Open();

                        cmd.Parameters.AddWithValue("CLoudId", product.Id);
                        cmd.Parameters.AddWithValue("Name", product.Name.Trim());
                        
                        cmd.Parameters.AddWithValue("Department", product.Department.Trim());
                        cmd.Parameters.AddWithValue("Material", product.Material.Trim());
                

                        if (!string.IsNullOrEmpty(product.Lab))
                            cmd.Parameters.AddWithValue("Lab", product.Lab.Trim());

                        cmd.Parameters.AddWithValue("Cost", product.Cost);
                        cmd.Parameters.AddWithValue("Unit", product.Unit);

                        if (product.GeneratedDate.HasValue)
                            cmd.Parameters.AddWithValue("GeneratedDate", product.GeneratedDate);

                        cmd.Parameters.AddWithValue("kafka_Topic", product.kafka_Topic);

                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
    }


    public interface IProducRepository
    {
        void Insert(ProductBE person);
        
    }
}
