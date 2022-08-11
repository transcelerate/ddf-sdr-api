using Microsoft.Extensions.Configuration;
using System;
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
            Config.isGroupFilterEnabled = Convert.ToBoolean(config.GetSection("isGroupFilterEnabled").Value);
            Config.isAuthEnabled = Convert.ToBoolean(config.GetSection("isAuthEnabled").Value);
        }
    }
}
