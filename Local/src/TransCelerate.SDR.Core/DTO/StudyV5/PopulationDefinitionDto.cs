using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class PopulationDefinitionDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }       
        public bool IncludesHealthySubjects { get; set; }       
        public RangeDto PlannedAge { get; set; }        
        public RangeDto PlannedCompletionNumber { get; set; }
        public RangeDto PlannedEnrollmentNumber { get; set; }
        public List<CodeDto> PlannedSex { get; set; }
        public List<EligibilityCriterionDto> Criterionids { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
