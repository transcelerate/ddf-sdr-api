using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class EndpointDto : IUuid
    {
        public string Uuid { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public List<CodeDto> EndpointLevel { get; set; }
    }
}
