using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class VersionCompareDto
    {
        public string StudyId { get; set; }
        [JsonProperty(nameof(LHS))]
        public VersionDetails LHS { get; set; }
        [JsonProperty(nameof(RHS))]
        public VersionDetails RHS { get; set; }
        public List<string> ElementsChanged { get; set; }
    }

    public class VersionDetails
    {
        public DateTime EntryDateTime { get; set; }

        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
    }
}
