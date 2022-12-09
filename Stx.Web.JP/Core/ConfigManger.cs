using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stx.Web.JP.Core
{
    public class ConfigManger
    {

        public void Initialize(IConfiguration configuration)
        {
        }

        static ConfigManger()
        {
            //IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            //var playerSection = configuration.GetSection(nameof(JPSettingOptions));
            //_apiVersion = playerSection...GetConnectionString("AP");
        }
        
        private static string _apiVersion;
        public string ApiVersion()
        {
            return _apiVersion;
        }



    }
}
