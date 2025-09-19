using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyDefinitionDocumentVersionDto
    {
        public string Id { get; set; }
        public string version { get; set; }
        public CodeDto status { get; set; }
        public List<GovernanceDateDto> DateValues { get; set; }
        public List<NarrativeContentDto> Contents { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public string InstanceType { get; set; }
    }
}
