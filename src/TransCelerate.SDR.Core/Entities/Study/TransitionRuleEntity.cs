using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class TransitionRuleEntity
    {
        [BsonElement("id")]
        public string transitionRuleId { get; set; }
        public string description { get; set; }
        public List<CodingEntity> coding { get; set; }
    }
}
