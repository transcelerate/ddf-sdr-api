using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyArmDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public List<CodeDto> StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public List<CodeDto> StudyArmType { get; set; }
    }
}
