using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pelsoft.Log.Common.Services;
using Pelsoft.Log.Common.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pelsoft.Log.Common
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add CnnStringService,PelsoftLogService,CommonWorkers
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region injections needed for all components 
            services.AddSingleton<ICnnStringService, CnnStringService>();
            services.AddSingleton<IPelsoftLogService, PelsoftLogService>();
            services.AddSingleton<CommonWorkers>();
            #endregion



            return services;
        }
    }
}
