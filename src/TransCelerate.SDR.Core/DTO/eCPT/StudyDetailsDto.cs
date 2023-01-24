using Newtonsoft.Json;
using TransCelerate.SDR.Core.DTO.Common;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public  class StudyDetailsDto
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        [JsonProperty("usdm-version")]
        public string UsdmVersion { get; set; }

        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
        public object Links { get; set; }
    }
}
