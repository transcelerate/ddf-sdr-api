using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyArmDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyArmId)]
        public string Id { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public List<CodeDto> StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public CodeDto StudyArmType { get; set; }
    }
}
