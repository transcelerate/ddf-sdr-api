using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;

namespace TransCelerate.SDR.WebApi.DependencyInjection
{
    public static class ApplicationDependencyInjector
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IClinicalStudyRepository, ClinicalStudyRepository>();
            services.AddTransient<IClinicalStudyService, ClinicalStudyService>();
            services.AddTransient<IUserGroupMappingRepository, UserGroupMappingRepository>();
            services.AddTransient<IUserGroupMappingService, UserGroupMappingService>();
            services.AddTransient<ILogHelper, LogHelper>();
            services.AddTransient<IHelper, Helper >();
            services.AddTransient<IClinicalStudyRepositoryV1, ClinicalStudyRepositoryV1>();
            services.AddTransient<IClinicalStudyServiceV1, ClinicalStudyServiceV1 >();
            services.AddTransient<IChangeAuditRepository, ChangeAuditRepository >();
            services.AddTransient<IChangeAuditService, ChangeAuditService >();
            services.AddTransient<IMongoClient, MongoClient>(db => new MongoClient(Config.ConnectionString));            

            return services;
        }
    }
}
