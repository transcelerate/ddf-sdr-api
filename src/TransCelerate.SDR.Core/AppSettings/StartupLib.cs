﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.AppSettings
{
    public static class StartupLib
    {
        public static void SetConstants(IConfiguration config)
        {            
            Config.connectionString = Convert.ToString(config.GetSection("ConnectionStrings:ServerName").Value);
            Config.databaseName = Convert.ToString(config.GetSection("ConnectionStrings:DatabaseName").Value);
            Config.instrumentationKey = Convert.ToString(config.GetSection("ApplicationInsights:InstrumentationKey").Value);
        }
    }
}
