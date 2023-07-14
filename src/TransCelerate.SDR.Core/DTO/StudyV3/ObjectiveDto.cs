using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ObjectiveDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.ObjectiveId)]
        public string Id { get; set; }
        public string ObjectiveDescription { get; set; }
        public CodeDto ObjectiveLevel { get; set; }
        public List<EndpointDto> ObjectiveEndpoints { get; set; }
    }
}
