using Newtonsoft.Json;
using System;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class AuditTrailDto
    {
        public DateTime EntryDateTime { get; set; }  
        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
    }
}
