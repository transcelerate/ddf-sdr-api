using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudySectionsDTO
    {
        public string studyId { get; set; }
        public int studyVersion { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<GetStudyDesignsDTO> studyDesigns { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<StudyObjectiveDTO> objectives { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<StudyIndicationDTO> studyIndications { get; set; }
        //Removed Study Protocol
        //public StudyProtocolDTO studyProtocol { get; set; }
    }
}
