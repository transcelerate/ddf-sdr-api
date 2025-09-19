using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyDefinitionDocumentDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeDto Type { get; set; }
        public CodeDto language { get; set; }
        public string templateName { get; set; }
        public List<string> ChildIds { get; set; }
        public List<StudyDefinitionDocumentVersionDto> Versions { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public string InstanceType { get; set; }
}
}
