using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    /// <summary>
    /// This class is a DTO for response of GET Study History Endpoint
    /// </summary>
    public class StudyHistoryResponseDto
    {
        public string StudyId { get; set; }
        [JsonProperty("SDRUploadVersion")]
        public List<UploadVersionDto> SDRUploadVersion { get; set; }
    }

    public class UploadVersionDto
    {
        public int UploadVersion { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public object StudyIdentifiers { get; set; }
        public List<string> ProtocolVersions { get; set; }
        public string UsdmVersion { get; set; }
        public LinksDto Links { get; set; }
    }
}
