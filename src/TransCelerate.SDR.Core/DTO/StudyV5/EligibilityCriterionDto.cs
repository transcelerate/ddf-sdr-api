using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class EligibilityCriterionDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public CodeDto Category { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public EligibilityCriterionItemDto CriterionItem { get; set; }    
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string ContextId { get; set; }
    }
}
