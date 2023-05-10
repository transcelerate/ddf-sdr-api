using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class InvestigationalInterventionDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.InvestigationalInterventionId)]
        public string Id { get; set; }
        public string InterventionDescription { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
