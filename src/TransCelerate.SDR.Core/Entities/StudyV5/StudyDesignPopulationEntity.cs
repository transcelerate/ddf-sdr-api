using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class StudyDesignPopulationEntity : PopulationDefinitionEntity
    {
        public List<StudyCohortEntity> Cohorts { get; set; }
    }
}
