using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class EligibilityCriterionEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public CodeEntity Category { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public string CriterionItemId { get; set; }    
        public string NextId { get; set; }
        public string PreviousId { get; set; }
    }
}
