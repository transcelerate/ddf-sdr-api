using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class ObjectiveDto : IUuid
    {
        public string Uuid { get; set; }
        public string ObjectiveDesc { get; set; }
        public List<CodeDto> ObjectiveLevel { get; set; }
        public List<EndpointDto> ObjectiveEndpoints { get; set; }
    }
}
