using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.WebApi
{
    public class Program
    {
        //static string env;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();                    
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builfConfig = config.Build();
                    var vaultName = builfConfig[Constants.KeyVault.Key];
                    var clientId = builfConfig[Constants.KeyVault.ClientId];
                    var clientSecret = builfConfig[Constants.KeyVault.ClientSecret];

                    //For deployed code
                    if (clientId == null)
                    {
                        var azureTokenProvider = new AzureServiceTokenProvider();
                        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback
                                                 (azureTokenProvider.KeyVaultTokenCallback));
                        config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
                    }
                    //For running the code in local
                    else
                    {
                        config.AddAzureKeyVault(vaultName, clientId, clientSecret);
                    }
                });
    }
}
