using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class IndicationDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.IndicationId)]
        public string Id { get; set; }
        public string IndicationDescription { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
