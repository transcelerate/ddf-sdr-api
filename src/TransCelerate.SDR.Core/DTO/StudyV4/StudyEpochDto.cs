using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyEpochDto : IId
    {        
        public string Id { get; set; }
        public string NextStudyEpochId { get; set; }
        public string PreviousStudyEpochId { get; set; }
        public string StudyEpochDescription { get; set; }
        public string StudyEpochName { get; set; }
        public CodeDto StudyEpochType { get; set; }
    }
}
