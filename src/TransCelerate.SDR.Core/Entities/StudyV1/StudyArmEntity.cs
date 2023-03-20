using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyArmEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyArmDataOriginDesc { get; set; }
        public List<CodeEntity> StudyArmDataOriginType { get; set; }
        public string StudyArmDesc { get; set; }
        public string StudyArmName { get; set; }
        public List<CodeEntity> StudyArmType { get; set; }
    }
}
