using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class RuleEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string RuleId { get; set; }
        public string Description { get; set; }
        public List<CodingEntity> Coding { get; set; }
    }
}
