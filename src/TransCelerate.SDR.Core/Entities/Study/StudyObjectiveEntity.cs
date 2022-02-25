using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyObjectiveEntity
    {
        public string description { get; set; }

        [BsonElement("id")]
        public string objectiveId { get; set; }

        public string level { get; set; }

        public List<EndpointsEntity> endpoints { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class EndpointsEntity
    {

        public string description { get; set; }

        public string purpose { get; set; }

        [BsonElement("id")]
        public string endPointsId { get; set; }

        public string outcomeLevel { get; set; }
    }            
}
