using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class PopulationDefinitionDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }       
        public bool IncludesHealthySubjects { get; set; }       
        public RangeDto PlannedAge { get; set; }        
        public QuantityDto PlannedCompletionNumber { get; set; }
        public QuantityDto PlannedEnrollmentNumber { get; set; }
        public List<CodeDto> PlannedSex { get; set; }
        public List<EligibilityCriterionDto> Criteria { get; set; }
        public string InstanceType { get; set; }
    }
}
