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
            Config.ConnectionString = Convert.ToString(config.GetSection("ConnectionStrings:DefaultConnection").Value);
            Config.DatabaseName = Convert.ToString(config.GetSection("ConnectionStrings:DatabaseName").Value);
            Config.DateRange = Convert.ToString(config.GetSection("StudyHistory:DateRange").Value);
            Config.ApiVersionUsdmVersionMapping = Convert.ToString(config.GetSection("ApiVersionUsdmVersionMapping").Value);
            Config.SdrCptMasterDataMapping = Convert.ToString(config.GetSection("SdrCptMasterDataMapping").Value);
            Config.ConformanceRules = Convert.ToString(config.GetSection("ConformanceRules").Value);
            Config.CdiscRulesEngine = Convert.ToString(config.GetSection("CdiscRulesEngine").Value);
            Config.CdiscRulesEngineRelativeBinary = Convert.ToString(config.GetSection("CdiscRulesEngineRelativeBinary").Value);
            Config.CdiscRulesEngineRelativeCache = Convert.ToString(config.GetSection("CdiscRulesEngineRelativeCache").Value);

            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(Config.ApiVersionUsdmVersionMapping);
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;

            Config.ConformanceRules = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ConformanceRules.json");
            ConformanceNonStatic conformanceNonStatic = JsonConvert.DeserializeObject<ConformanceNonStatic>(Config.ConformanceRules);
            Conformance.ConformanceRules = conformanceNonStatic.ConformanceRules;

            if (String.IsNullOrWhiteSpace(Config.SdrCptMasterDataMapping))
                Config.SdrCptMasterDataMapping = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SdrCptMasterDataMapping.json");

            SdrCptMapping_NonStatic sdrCptMapping_NonStatic = JsonConvert.DeserializeObject<SdrCptMapping_NonStatic>(Config.SdrCptMasterDataMapping);
            SdrCptMapping.SdrCptMasterDataMapping = sdrCptMapping_NonStatic.SdrCptMasterDataMapping;
        }
    }
}
