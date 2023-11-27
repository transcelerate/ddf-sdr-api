using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ObjectiveDto : SyntaxTemplateDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.OBJECTIVE);
        public CodeDto Level { get; set; }
        public List<EndpointDto> Endpoints { get; set; }
    }
}
