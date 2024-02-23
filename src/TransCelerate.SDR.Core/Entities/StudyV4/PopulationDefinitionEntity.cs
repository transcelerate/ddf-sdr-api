using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class PopulationDefinitionEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IncludesHealthySubjects { get; set; }
        public RangeEntity PlannedAge { get; set; }        
        public QuantityEntity PlannedCompletionNumber { get; set; }
        public QuantityEntity PlannedEnrollmentNumber { get; set; }
        public List<CodeEntity> PlannedSex { get; set; }
        public List<EligibilityCriterionEntity> Criteria { get; set; }
        public string InstanceType { get; set; }
    }
}
