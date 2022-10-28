using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class InvestigationalInterventionDto : IUuid
    {
        public string Uuid { get; set; }
        public string InterventionDesc { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
