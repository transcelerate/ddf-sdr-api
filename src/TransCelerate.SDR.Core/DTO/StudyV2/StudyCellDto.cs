using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyCellDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyCellId)]
        public string Id { get; set; }
        public StudyArmDto StudyArm { get; set; }
        public StudyEpochDto StudyEpoch { get; set; }
        public List<StudyElementDto> StudyElements { get; set; }
    }
}
