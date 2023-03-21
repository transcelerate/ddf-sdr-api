using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class ItemEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string ItemId { get; set; }
        public string Description { get; set; }
        public PointInTimeEntity FromPointInTime { get; set; }
        public PointInTimeEntity ToPointInTime { get; set; }
        public ActivityEntity Activity { get; set; }
        public EncounterEntity Encounter { get; set; }
        public List<string> PreviousItemsInSequence { get; set; }
        public List<string> NextItemsInSequence { get; set; }
    }
}
