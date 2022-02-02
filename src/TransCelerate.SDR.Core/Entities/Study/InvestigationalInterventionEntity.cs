using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.Study
{
    

    [BsonIgnoreExtraElements]
    public class InvestigationalInterventionEntity
    {
        [BsonElement("id")]     
        public string investigationalInterventionId { get; set; }

        public string description { get; set; }

        public string interventionType { get; set; }
        public List<CodingEntity> coding { get; set; }
    }
}
