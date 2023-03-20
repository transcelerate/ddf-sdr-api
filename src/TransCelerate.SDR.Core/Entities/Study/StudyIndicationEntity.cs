using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{

    [BsonIgnoreExtraElements]
    public class StudyIndicationEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string StudyIndicationId { get; set; }

        public string Description { get; set; }

        public List<CodingEntity> Coding { get; set; }
    }
}
