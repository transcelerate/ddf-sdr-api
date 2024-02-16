using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EligibilityCriteriaDto : SyntaxTemplateDto
    {        
        public CodeDto Category { get; set; }
        public string Identifier { get; set; }
        public string PreviousId { get; set; }
        public string NextId { get; set; }
        public string ContextId { get; set; }        
    }
}
