
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace HQQLibrary.Model.Utilities
{
    public class AppConfiguration
    {
        public readonly string _connectionString = string.Empty;
        private IConfigurationRoot configRoot;
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            configRoot = configurationBuilder.Build();
            _connectionString = configRoot.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
           // var appSetting = configRoot.GetSection("ApplicationSettings");
        }
        public string ConnectionString
        {
            get => _connectionString;
        }

        public string GetConnectionString(string name)
        {
            return configRoot.GetSection("ConnectionStrings").GetSection(name).Value;
        }

        public string GetAppSettings(string name)
        {
            return configRoot.GetSection("AppSettings").GetSection(name).Value;
        }

    }
}
