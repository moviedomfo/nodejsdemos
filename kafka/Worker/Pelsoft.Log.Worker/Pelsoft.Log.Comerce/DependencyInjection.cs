using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pelsoft.Log.Comerce.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pelsoft.Log.Comerce
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddComerceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProducRepository, ProductRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();

            

            return services;
        }
    }
}
