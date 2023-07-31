using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ObjectiveDto : IId
    {        
        public string Id { get; set; }
        public string ObjectiveDescription { get; set; }
        public CodeDto ObjectiveLevel { get; set; }
        public List<EndpointDto> ObjectiveEndpoints { get; set; }
    }
}
