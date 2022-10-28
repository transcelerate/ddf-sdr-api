using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyArmDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyArmDataOriginDesc { get; set; }
        public List<CodeDto> StudyArmDataOriginType { get; set; }
        public string StudyArmDesc { get; set; }
        public string StudyArmName { get; set; }
        public List<CodeDto> StudyArmType { get; set; }
    }
}
