using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
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

            var vaultName = System.Environment.GetEnvironmentVariable("KeyVaultName");
            var config = new ConfigurationBuilder().AddEnvironmentVariables();
            var azureTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback
                                     (azureTokenProvider.KeyVaultTokenCallback));
            config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
            var buildConfig = config.Build();

            Config.ConnectionString = Convert.ToString(buildConfig["ConnectionStrings:ServerName"]);
            Config.DatabaseName = Convert.ToString(buildConfig["ConnectionStrings:DatabaseName"]);                       

            builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();
            builder.Services.AddTransient<ILogHelper, LogHelper>();
            builder.Services.AddTransient<IHelperV2, HelperV2>();
            builder.Services.AddTransient<IChangeAuditRepository, ChangeAuditRepository>();
            builder.Services.AddTransient<IMongoClient, MongoClient>(db => new MongoClient(Config.ConnectionString));
        }
    }
}
