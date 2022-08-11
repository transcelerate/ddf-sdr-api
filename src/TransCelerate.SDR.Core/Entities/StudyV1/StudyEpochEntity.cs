using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyEpochEntity
    {
        public string Uuid { get; set; }
        public string nextStudyEpochId { get; set; }
        public string previousStudyEpochId { get; set; }
        public string StudyEpochDesc { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeEntity> StudyEpochType { get; set; }        
    }
}
