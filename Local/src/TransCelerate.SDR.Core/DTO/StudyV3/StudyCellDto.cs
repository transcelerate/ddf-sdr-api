using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyCellDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.StudyCellId)]
        public string Id { get; set; }
        public string StudyArmId { get; set; }
        public string StudyEpochId { get; set; }
        public List<string> StudyElementIds { get; set; }
    }
}
