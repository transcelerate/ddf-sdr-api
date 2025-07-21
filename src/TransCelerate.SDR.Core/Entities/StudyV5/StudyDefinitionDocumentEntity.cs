using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyDefinitionDocumentEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeEntity Type { get; set; }
        public CodeEntity language { get; set; }
        public string templateName { get; set; }
        public List<string> ChildIds { get; set; }
		public List<StudyDefinitionDocumentVersionEntity> Versions { get; set; }
		public List<CommentAnnotationEntity> Notes { get; set; }
        public string InstanceType { get; set; }
    }
}
