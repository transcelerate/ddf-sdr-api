using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ConditionDto : SyntaxTemplateDto
    {
        public List<string> ContextIds { get; set; }
        public List<string> AppliesToIds { get; set; }
    }
}
