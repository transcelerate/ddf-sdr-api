using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class LinksResponseDto
    {
        public string StudyId { get; set; }
        public string UsdmVersion { get; set; }
        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        public LinksEndpointDto Links { get; set; }
    }
}
