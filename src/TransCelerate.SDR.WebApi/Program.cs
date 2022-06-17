using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using System;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.WebApi
{
    public class Program
    {   
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

                    if (!context.HostingEnvironment.IsDevelopment())
                    {
                        //For deployed code
                        if (!String.IsNullOrEmpty(vaultName))
                        {
                            var azureTokenProvider = new AzureServiceTokenProvider();
                            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback
                                                     (azureTokenProvider.KeyVaultTokenCallback));
                            config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
                        }
                    }                    
                    else
                    {
                        //For getting key vault values when running the code in local:
                        //          Need to add vault Name, client Id and client secret of registered app which is linked to keyvault
                        //          and uncomment below if block.
                        //if (!String.IsNullOrEmpty(vaultName) && !String.IsNullOrEmpty(clientId) && !String.IsNullOrEmpty(clientId))
                        //{
                        //    config.AddAzureKeyVault(vaultName, clientId, clientSecret);
                        //}
                    }

                });
    }
}
