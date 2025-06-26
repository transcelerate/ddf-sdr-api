using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyAmendmentImpactEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public bool IsSubstantial { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public CodeEntity Type { get; set; }
    }
}
