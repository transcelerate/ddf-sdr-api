using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class StudyDetailsDto
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string UsdmVersion { get; set; }

        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        public object Links { get; set; }
    }
}
