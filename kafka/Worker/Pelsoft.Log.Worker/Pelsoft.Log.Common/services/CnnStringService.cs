using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common.Services
{
    public class CnnStringService: ICnnStringService
    {
        private IConfiguration Configuration;

        public CnnStringService(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public  string GetCnnString_byName(string cnnName)
        {
            
            return Configuration.GetConnectionString(cnnName);
        }
    }

    public interface ICnnStringService
    {
        string GetCnnString_byName(string cnnStringName);
    }

 
}
