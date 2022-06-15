namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds the value from keyvault which are fetched at runtime
    /// </summary>
    public static class Config
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static string InstrumentationKey { get; set; }
        public static string DateRange { get; set; }
        public static string Audience { get; set; }
        public static string TenantID { get; set; }
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        public static string Authority { get; set; }
        public static string Scope { get; set; }
        public static string UserName { get; set; }
        public static string UserRole { get; set; }
        public static bool isGroupFilterEnabled { get; set; }
        public static bool isAuthEnabled { get; set; }
    }
}
