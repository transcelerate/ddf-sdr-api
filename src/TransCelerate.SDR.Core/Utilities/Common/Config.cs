using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class Config
    {
        public static string clientID { get; set; }
        public static string domain { get; set; }
        public static string tenantID { get; set; }
        public static string instance { get; set; }

        public static string connectionString { get; set; }
        public static string databaseName { get; set; }
        
        public static string instrumentationKey { get; set; }
    }
}
