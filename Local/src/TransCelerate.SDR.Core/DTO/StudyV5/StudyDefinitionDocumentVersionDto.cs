using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    internal class StudyDefinitionDocumentVersionDto
    {
        public string Id { get; set; }
        public string version { get; set; }
        public CodeDto status { get; set; }
        public List<GovernanceDateDto> Datevalues { get; set; }
        public List<NarrativeContentDto> Contents { get; set; }
        public List<string> ChildIds { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public string InstanceType { get; set; }
    }
}
