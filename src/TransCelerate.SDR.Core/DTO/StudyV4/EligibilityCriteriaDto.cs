using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EligibilityCriteriaDto : SyntaxTemplateDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.ELIGIBILITY_CRITERIA);
        public CodeDto Category { get; set; }
        public string Identifier { get; set; }
        public string PreviousId { get; set; }
        public string NextId { get; set; }
    }
}
