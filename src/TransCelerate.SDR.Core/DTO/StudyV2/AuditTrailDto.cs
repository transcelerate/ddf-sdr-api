using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class AuditTrailDto
    {
        public string EntryDateTime { get; set; }  
        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
    }
}
