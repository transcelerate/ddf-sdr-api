﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class ApiUsdmVersionMapping
    {
        [JsonProperty(IdFieldPropertyName.Common.SDRVersions)]
        public static List<SDRVersions> SDRVersions { get; set; }
    }

    public class SDRVersions
    {
        public string ApiVersion { get; set; }

        public List<string> UsdmVersions { get; set; }
    }

    public class ApiUsdmVersionMapping_NonStatic
    {
        [JsonProperty(IdFieldPropertyName.Common.SDRVersions)]
        public List<SDRVersions> SDRVersions { get; set; }
    }
}
