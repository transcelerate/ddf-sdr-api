using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class BiomedicalConceptCategoryDto : IId
    {        
        public string Id { get; set; }
        public List<string> ChildIds  { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<string> MemberIds { get; set; }
        public AliasCodeDto Code { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
