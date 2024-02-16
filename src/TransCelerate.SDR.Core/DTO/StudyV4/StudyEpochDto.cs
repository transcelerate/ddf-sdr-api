using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyEpochDto : IId
    {        
        public string Id { get; set; }        
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeDto Type { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string InstanceType { get; set; }
    }
}
