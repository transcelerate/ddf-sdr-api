using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class AuditTrailDTO
    {
        public string EntryDateTime { get; set; }
        public string EntrySystem { get; set; }
        [JsonProperty(nameof(DTO.Common.AuditTrailDto.SDRUploadVersion))]
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
