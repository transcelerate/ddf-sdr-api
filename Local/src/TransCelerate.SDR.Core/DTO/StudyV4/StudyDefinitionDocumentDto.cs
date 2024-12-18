using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    internal class StudyDefinitionDocumentDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeDto Type { get; set; }
        public CodeDto language { get; set; }
        public string templateName { get; set; }
        public List<StudyDefinitionDocumentVersionDto> versions { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public string InstanceType { get; set; }
}
}
