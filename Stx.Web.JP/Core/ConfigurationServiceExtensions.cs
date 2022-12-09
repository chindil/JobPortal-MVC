using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stx.Web.JP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection //Do not change the namespace
{
    public static class ConfigurationServiceExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JPSettingOptions>(config.GetSection(JPSettingOptions.JPSetting));

            return services;
        }
    }
}
