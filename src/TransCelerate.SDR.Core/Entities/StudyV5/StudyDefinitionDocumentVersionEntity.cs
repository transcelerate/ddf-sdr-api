using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyDefinitionDocumentVersionEntity : IId
	{
        public string Id { get; set; }
        public string version { get; set; }
        public CodeEntity status { get; set; }
        public List<GovernanceDateEntity> DateValues { get; set; }
        public List<NarrativeContentEntity> Contents { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public string InstanceType { get; set; }
    }
}
