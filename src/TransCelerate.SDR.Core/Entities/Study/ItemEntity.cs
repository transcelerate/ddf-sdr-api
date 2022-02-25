using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class ItemEntity
    {
        [BsonElement("id")]
        public string itemId { get; set; }
        public string description { get; set; }
        public PointInTimeEntity fromPointInTime { get; set; }
        public PointInTimeEntity toPointInTime { get; set; }
        public ActivityEntity activity { get; set; }
        public EncounterEntity encounter { get; set; }
        public List<string> previousItemsInSequence { get; set; }
        public List<string> nextItemsInSequence { get; set; }
    }
}
