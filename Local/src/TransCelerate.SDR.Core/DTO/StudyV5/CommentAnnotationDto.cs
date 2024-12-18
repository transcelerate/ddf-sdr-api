using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class CommentAnnotationDto : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<CodeDto> Codes { get; set; }
        public string InstanceType { get; set; }
    }
}
