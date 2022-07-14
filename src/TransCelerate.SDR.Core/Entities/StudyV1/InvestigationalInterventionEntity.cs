using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class InvestigationalInterventionEntity
    {
        public string Uuid { get; set; }
        public string InterventionDesc { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
