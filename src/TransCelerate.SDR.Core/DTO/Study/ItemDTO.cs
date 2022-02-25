using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ItemDTO
    {
        public string id { get; set; }
        public string description { get; set; }
        public PointInTimeDTO fromPointInTime { get; set; }
        public PointInTimeDTO toPointInTime { get; set; }
        public ActivityDTO activity { get; set; }
        public EncounterDTO encounter { get; set; }
        public List<string> previousItemsInSequence { get; set; }
        public List<string> nextItemsInSequence { get; set; }
    }
}
