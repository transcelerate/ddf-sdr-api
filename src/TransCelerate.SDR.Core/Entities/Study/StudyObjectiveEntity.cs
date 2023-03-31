using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyObjectiveEntity
    {
        public string Description { get; set; }

        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string ObjectiveId { get; set; }

        public string Level { get; set; }

        public List<EndpointsEntity> Endpoints { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class EndpointsEntity
    {

        public string Description { get; set; }

        public string Purpose { get; set; }

        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string EndPointsId { get; set; }

        public string OutcomeLevel { get; set; }
    }
}
