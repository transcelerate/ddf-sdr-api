namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds the value from keyvault which are fetched at runtime
    /// </summary>
    public static class Config
    {        
        public static string connectionString { get; set; }
        public static string databaseName { get; set; }        
        public static string instrumentationKey { get; set; }
    }
}
