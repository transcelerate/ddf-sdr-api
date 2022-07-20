using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyEpochDto
    {
        public string Uuid { get; set; }
        public string nextStudyEpochId { get; set; }
        public string previousStudyEpochId { get; set; }
        public string StudyEpochDesc { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeDto> StudyEpochType { get; set; }
    }
}
