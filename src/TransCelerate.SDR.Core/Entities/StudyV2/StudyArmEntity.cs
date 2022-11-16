using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class StudyArmEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public List<CodeEntity> StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public List<CodeEntity> StudyArmType { get; set; }        
    }
}
