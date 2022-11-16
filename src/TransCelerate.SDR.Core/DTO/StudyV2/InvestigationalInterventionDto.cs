using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class InvestigationalInterventionDto : IUuid
    {
        public string Uuid { get; set; }
        public string InterventionDescription{ get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
