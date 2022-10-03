using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
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

[assembly: FunctionsStartup(typeof(TransCelerate.SDR.AzureFunctions.Startup))]
namespace TransCelerate.SDR.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Config.ConnectionString = System.Environment.GetEnvironmentVariable("ConnectionStrings:ServerName");
            Config.DatabaseName = System.Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseName");
            
            builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();
            builder.Services.AddTransient<ILogHelper, LogHelper>();
            builder.Services.AddTransient<IChangeAuditReposotory, ChangeAuditReposotory>();
            builder.Services.AddTransient<IMongoClient, MongoClient>(db => new MongoClient(Config.ConnectionString));
        }
    }
}
