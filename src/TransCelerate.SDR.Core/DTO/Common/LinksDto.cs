using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class LinksDto
    {
        public string StudyDefinitions { get; set; }
        public string AuditTrail { get; set; }
        
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
        public string AuditTrail { get; set; }

        public List<StudyDesignLinks> StudyDesigns { get; set; }

        [JsonProperty("SoA")]
        public string SoA { get; set; }
    }
    public class LinksEndpointDto
    {
        public string StudyDefinitions { get; set; }
        public string AuditTrail { get; set; }
        [JsonProperty("SoA")]
        public string SoA { get; set; }
    }
}
