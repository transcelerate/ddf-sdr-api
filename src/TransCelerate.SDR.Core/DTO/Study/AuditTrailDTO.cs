using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class AuditTrailDTO
    {               
        public string entryDateTime { get; set; }     
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
        [JsonProperty("usdm-version")]
        public string UsdmVersion { get; set; }
    }
}
