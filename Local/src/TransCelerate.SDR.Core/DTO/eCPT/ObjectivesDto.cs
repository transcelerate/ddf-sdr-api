using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class ObjectivesDto
    {
        public string ObjectiveDescription { get; set; }
        public string ObjectiveLevel { get; set; }
        public List<ObjectiveEndpointDto> ObjectiveEndpoints { get; set; }
    }
}
