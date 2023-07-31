using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyCellDto : IId
    {        
        public string Id { get; set; }
        public string StudyArmId { get; set; }
        public string StudyEpochId { get; set; }
        public List<string> StudyElementIds { get; set; }
    }
}
