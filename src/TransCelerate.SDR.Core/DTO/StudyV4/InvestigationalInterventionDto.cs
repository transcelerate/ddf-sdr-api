using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class InvestigationalInterventionDto : IId
    {        
        public string Id { get; set; }
        public string InterventionDescription { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
