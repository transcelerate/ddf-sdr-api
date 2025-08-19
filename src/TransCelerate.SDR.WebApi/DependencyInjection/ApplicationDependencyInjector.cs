using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.RuleEngine.Utilities.Interceptors;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;

namespace TransCelerate.SDR.WebApi.DependencyInjection
{
    public static class ApplicationDependencyInjector
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<ILogHelper, LogHelper>();            
            //services.AddTransient<IHelperV2, HelperV2>();
            services.AddTransient<IHelperV3, HelperV3>();
            services.AddTransient<IHelperV4, HelperV4>();
            services.AddTransient<IHelperV5, HelperV5>();
            //services.AddTransient<IStudyRepositoryV2, StudyRepositoryV2>();
            services.AddTransient<IStudyRepositoryV3, StudyRepositoryV3>();
            services.AddTransient<IStudyRepositoryV4, StudyRepositoryV4>();
            services.AddTransient<IStudyRepositoryV5, StudyRepositoryV5>();
            //services.AddTransient<IStudyServiceV2, StudyServiceV2>();
            services.AddTransient<IStudyServiceV3, StudyServiceV3>();
            services.AddTransient<IStudyServiceV4, StudyServiceV4>();
            services.AddTransient<IStudyServiceV5, StudyServiceV5>();
            services.AddTransient<IChangeAuditRepository, ChangeAuditRepository>();
            services.AddTransient<IChangeAuditService, ChangeAuditService>();
            services.AddTransient<ICommonService, CommonServices>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            // Added because MongoDB 2.19 version by default supports LinqProvider.V3
            var clientSettings = MongoClientSettings.FromConnectionString(Config.ConnectionString);
            clientSettings.LinqProvider = LinqProvider.V2;
            // Added because MongoDB 2.19 version by default supports LinqProvider.V3
            services.AddSingleton<IMongoClient, MongoClient>(db => new MongoClient(clientSettings));

            services.AddSingleton<IValidatorInterceptor, RuleValidatorInterceptor>();

            return services;
        }
    }
}
