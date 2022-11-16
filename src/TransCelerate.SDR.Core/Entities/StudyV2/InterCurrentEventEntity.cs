using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class InterCurrentEventEntity : IUuid
    {
        public string Uuid { get; set; }
        public string IntercurrentEventDescription { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
