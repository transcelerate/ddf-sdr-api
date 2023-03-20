using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ItemDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public PointInTimeDTO FromPointInTime { get; set; }
        public PointInTimeDTO ToPointInTime { get; set; }
        public ActivityDTO Activity { get; set; }
        public EncounterDTO Encounter { get; set; }
        public List<string> PreviousItemsInSequence { get; set; }
        public List<string> NextItemsInSequence { get; set; }
    }
}
