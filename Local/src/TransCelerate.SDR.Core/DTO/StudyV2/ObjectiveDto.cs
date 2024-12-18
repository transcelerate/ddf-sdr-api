using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ObjectiveDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ObjectiveId)]
        public string Id { get; set; }
        public string ObjectiveDescription { get; set; }
        public CodeDto ObjectiveLevel { get; set; }
        public List<EndpointDto> ObjectiveEndpoints { get; set; }
    }
}
