using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{


    [BsonIgnoreExtraElements]
    public class InvestigationalInterventionEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string InvestigationalInterventionId { get; set; }

        public string Description { get; set; }
        public string InterventionModel { get; set; }
        public string Status { get; set; }
        public List<CodingEntity> Coding { get; set; }
    }
}
