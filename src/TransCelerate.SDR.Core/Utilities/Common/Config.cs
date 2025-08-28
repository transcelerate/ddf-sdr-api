namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds the value from keyvault which are fetched at runtime
    /// </summary>
    public static class Config
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static string DateRange { get; set; }
        public static string ApiVersionUsdmVersionMapping { get; set; }
        public static string ConformanceRules { get; set; }
        public static string SdrCptMasterDataMapping { get; set; }
        public static string CdiscRulesEngine { get; set; }
    }
}
