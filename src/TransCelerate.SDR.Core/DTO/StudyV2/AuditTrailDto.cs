using Newtonsoft.Json;
using System;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class AuditTrailDto
    {
        public DateTime EntryDateTime { get; set; }

        public string UsdmVersion { get; set; }

        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        //************* Added by basha

        [JsonProperty(nameof(SDRUploadFlag))]
        public int SDRUploadFlag { get; set; }
    }
}
