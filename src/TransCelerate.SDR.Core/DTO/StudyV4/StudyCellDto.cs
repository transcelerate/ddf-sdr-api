using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyCellDto : IId
    {        
        public string Id { get; set; }
        public string ArmId { get; set; }
        public string EpochId { get; set; }
        public List<string> ElementIds { get; set; }
    }
}
