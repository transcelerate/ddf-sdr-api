using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class AuditTrailResponseWithLinksDto
    {
        public DateTime EntryDateTime { get; set; }

        [JsonProperty("usdm-version")]
        public string UsdmVersion { get; set; }

        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
        public LinksDto Links { get; set; }
    }
}
