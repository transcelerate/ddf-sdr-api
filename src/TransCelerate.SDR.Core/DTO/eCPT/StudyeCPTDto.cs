using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.Common;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class StudyeCPTDto
    {
        public string  StudyId { get; set; }
        public string  StudyTitle { get; set; }
        public string StudyDesignId { get; set; }
        public string StudyDesignName { get; set; }

        [JsonProperty("usdm-version")]
        public string UsdmVersion { get; set; }

        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
        public LinksDto Links { get; set; }
        public eCPTDataDto ECPTData { get; set; }
    }
}
