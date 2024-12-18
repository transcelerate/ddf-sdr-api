using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class NarrativeContentItemDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string InstanceType { get; set; }
    }
}
