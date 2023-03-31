using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.AppSettings
{
    public static class StartupLib
    {
        /// <summary>
        /// Get From appsettings.json at runtime
        /// </summary>
        /// <param name="config">IConfiguration parameter</param>
        public static void SetConstants(IConfiguration config)
        {
            Config.Audience = Convert.ToString(config.GetSection("AzureAD:Audience").Value);
            Config.TenantID = Convert.ToString(config.GetSection("AzureAd-TenantId").Value);
            Config.Authority = Convert.ToString(config.GetSection("AzureAD-Authority").Value);
            Config.ClientSecret = Convert.ToString(config.GetSection("AzureAD-ClientSecret").Value);
            Config.ClientId = Convert.ToString(config.GetSection("AzureAD-ClientId").Value);
            Config.Scope = Convert.ToString(config.GetSection("AzureAD-Audience").Value);
            Config.ConnectionString = Convert.ToString(config.GetSection("ConnectionStrings:ServerName").Value);
            Config.DatabaseName = Convert.ToString(config.GetSection("ConnectionStrings:DatabaseName").Value);
            Config.InstrumentationKey = Convert.ToString(config.GetSection("ApplicationInsights:InstrumentationKey").Value);
            Config.AppInsightsApiKey = Convert.ToString(config.GetSection("AppInsights-ApiKey").Value);
            Config.AppInsightsAppId = Convert.ToString(config.GetSection("AppInsights-AppId").Value);
            Config.AppInsightsRESTApiUrl = Convert.ToString(config.GetSection("AppInsights-RESTApiUrl").Value);
            Config.DateRange = Convert.ToString(config.GetSection("StudyHistory:DateRange").Value);
            Config.IsGroupFilterEnabled = Convert.ToBoolean(config.GetSection("isGroupFilterEnabled").Value);
            Config.IsAuthEnabled = Convert.ToBoolean(config.GetSection("isAuthEnabled").Value);
            Config.AzureServiceBusConnectionString = Convert.ToString(config.GetSection("AzureServiceBusConnectionString").Value);
            Config.AzureServiceBusQueueName = Convert.ToString(config.GetSection("AzureServiceBusQueueName").Value);
            Config.ApiVersionUsdmVersionMapping = Convert.ToString(config.GetSection("ApiVersionUsdmVersionMapping").Value);
            Config.SdrCptMasterDataMapping = Convert.ToString(config.GetSection("SdrCptMasterDataMapping").Value);
            Config.ConformanceRules = Convert.ToString(config.GetSection("ConformanceRules").Value);

            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(Config.ApiVersionUsdmVersionMapping);
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;

            if (String.IsNullOrWhiteSpace(Config.ConformanceRules))
                Config.ConformanceRules = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ConformanceRules.json");

            ConformanceNonStatic conformanceNonStatic = JsonConvert.DeserializeObject<ConformanceNonStatic>(Config.ConformanceRules);
            Conformance.ConformanceRules = conformanceNonStatic.ConformanceRules;

            if (String.IsNullOrWhiteSpace(Config.SdrCptMasterDataMapping))
                Config.SdrCptMasterDataMapping = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SdrCptMasterDataMapping.json");

            SdrCptMapping_NonStatic sdrCptMapping_NonStatic = JsonConvert.DeserializeObject<SdrCptMapping_NonStatic>(Config.SdrCptMasterDataMapping);
            SdrCptMapping.SdrCptMasterDataMapping = sdrCptMapping_NonStatic.SdrCptMasterDataMapping;
        }
    }
}
