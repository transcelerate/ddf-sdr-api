using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class InterCurrentEventDto : IUuid
    {
        public string Uuid { get; set; }
        public string IntercurrentEventDesc { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
