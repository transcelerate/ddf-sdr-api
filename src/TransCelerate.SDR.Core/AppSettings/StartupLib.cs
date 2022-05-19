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
            Config.connectionString = Convert.ToString(config.GetSection("ConnectionStrings:ServerName").Value);
            Config.databaseName = Convert.ToString(config.GetSection("ConnectionStrings:DatabaseName").Value);
            Config.instrumentationKey = Convert.ToString(config.GetSection("ApplicationInsights:InstrumentationKey").Value);
            Config.dateRange = Convert.ToString(config.GetSection("StudyHistory:DateRange").Value);
        }
    }
}
