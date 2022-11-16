using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ObjectiveDto : IUuid
    {
        public string Uuid { get; set; }
        public string ObjectiveDescription { get; set; }
        public List<CodeDto> ObjectiveLevel { get; set; }
        public List<EndpointDto> ObjectiveEndpoints { get; set; }
    }
}
