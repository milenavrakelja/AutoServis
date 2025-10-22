using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutoServis.Core
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(GetBasePath())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    _configuration = builder.Build();
                }
                return _configuration;
            }
        }

        private static string GetBasePath()
        {
            // U WPF, AppDomain.CurrentDomain.BaseDirectory pokazuje na bin/Debug/netX.X/
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetConnectionString(string name = "DefaultConnection")
        {
            return Configuration.GetConnectionString(name);
        }
    }
}