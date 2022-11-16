using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class InterCurrentEventDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.IntercurrentEventId)]
        public string Id { get; set; }
        public string IntercurrentEventDescription { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
