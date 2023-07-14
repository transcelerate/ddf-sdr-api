using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class LinksDto
    {
        public string StudyDefinitions { get; set; }
        public string RevisionHistory { get; set; }

        public List<StudyDesignLinks> StudyDesigns { get; set; }
    }

    public class StudyDesignLinks
    {
        public string StudyDesignId { get; set; }
        public string StudyDesignLink { get; set; }
    }

    public class LinksForUIDto
    {
        public string StudyDefinitions { get; set; }
        public string RevisionHistory { get; set; }

        public List<StudyDesignLinks> StudyDesigns { get; set; }

        [JsonProperty(nameof(SoA))]
        public string SoA { get; set; }
    }
    public class LinksEndpointDto
    {
        public string StudyDefinitions { get; set; }
        public string RevisionHistory { get; set; }
        [JsonProperty(nameof(SoA))]
        public string SoA { get; set; }
    }
}
