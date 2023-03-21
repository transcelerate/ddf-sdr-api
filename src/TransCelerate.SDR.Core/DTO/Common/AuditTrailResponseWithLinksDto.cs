using Newtonsoft.Json;
using System;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class AuditTrailResponseWithLinksDto
    {
        public DateTime EntryDateTime { get; set; }

        public string UsdmVersion { get; set; }

        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        public LinksForUIDto Links { get; set; }
    }
}
