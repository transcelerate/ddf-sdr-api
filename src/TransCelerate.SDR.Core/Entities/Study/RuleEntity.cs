using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class RuleEntity
    {
        [BsonElement("id")]
        public string RuleId { get; set; }
        public string description { get; set; }
        public List<CodingEntity> coding { get; set; }
    }
}
