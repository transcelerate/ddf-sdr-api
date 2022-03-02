using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudyDesignsDTO
    {
        public string studyDesignId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PlannedWorkflowDTO> plannedWorkflows { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<StudyPopulationDTO> studyPopulations { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<StudyCellDTO> studyCells { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<InvestigationalInterventionDTO> investigationalInterventions { get; set; }
    }
}
