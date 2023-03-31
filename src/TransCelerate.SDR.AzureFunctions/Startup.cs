﻿using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;

[assembly: FunctionsStartup(typeof(TransCelerate.SDR.AzureFunctions.Startup))]
namespace TransCelerate.SDR.AzureFunctions
{
    /// <summary>
    /// Startup for Azure Function App
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        /// Add depenedncies for Azure Function App
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development") // Only for running in Local
            {
                Config.ConnectionString = System.Environment.GetEnvironmentVariable("ConnectionStrings:ServerName");
                Config.DatabaseName = System.Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseName");
                Config.ApiVersionUsdmVersionMapping = System.Environment.GetEnvironmentVariable("ApiVersionUsdmVersionMapping");
            }
            else
            {
                var vaultName = System.Environment.GetEnvironmentVariable("KeyVaultName");
                var config = new ConfigurationBuilder().AddEnvironmentVariables();
                var client = new SecretClient(new Uri(vaultName), new DefaultAzureCredential());
                config.AddAzureKeyVault(client: client, new KeyVaultSecretManager());
                var buildConfig = config.Build();



                Config.ConnectionString = Convert.ToString(buildConfig["ConnectionStrings:ServerName"]);
                Config.DatabaseName = Convert.ToString(buildConfig["ConnectionStrings:DatabaseName"]);
                Config.ApiVersionUsdmVersionMapping = Convert.ToString(buildConfig["ApiVersionUsdmVersionMapping"]);
            }

            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(Config.ApiVersionUsdmVersionMapping);
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;

            builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();
            builder.Services.AddTransient<ILogHelper, LogHelper>();
            builder.Services.AddTransient<IHelperV2, HelperV2>();
            builder.Services.AddTransient<IChangeAuditRepository, ChangeAuditRepository>();
            // Added because MongoDB 2.19 version by default supports LinqProvider.V3
            var clientSettings = MongoClientSettings.FromConnectionString(Config.ConnectionString);
            clientSettings.LinqProvider = LinqProvider.V2;
            // Added because MongoDB 2.19 version by default supports LinqProvider.V3
            builder.Services.AddTransient<IMongoClient, MongoClient>(db => new MongoClient(clientSettings));
        }
    }
}
