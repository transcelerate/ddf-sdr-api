

using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class ObjectivesEndpointsAndEstimandsDto
    {
        public List<ObjectivesDto> PrimaryObjectives { get; set; }
        public List<ObjectivesDto> SecondaryObjectives { get; set; }
    }
}
