using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyAmendmentImpactDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public object IsSubstantial { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public CodeDto Type { get; set; }
    }
}
