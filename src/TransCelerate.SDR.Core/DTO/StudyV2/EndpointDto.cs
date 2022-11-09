using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class EndpointDto : IUuid
    {
        public string Uuid { get; set; }
        public string EndpointDesc { get; set; }
        public string EndpointPurposeDesc { get; set; }
        public List<CodeDto> EndpointLevel { get; set; }
    }
}
