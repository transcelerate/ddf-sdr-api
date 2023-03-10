using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class LinksResponseDto
    {
        public string StudyId { get; set; }   
        public string UsdmVersion { get; set; }
        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
        public LinksEndpointDto Links { get; set; }
    }
}
