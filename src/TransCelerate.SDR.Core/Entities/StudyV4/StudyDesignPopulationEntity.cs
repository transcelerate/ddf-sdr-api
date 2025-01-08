using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDesignPopulationEntity : PopulationDefinitionEntity
    {
        public List<StudyCohortEntity> Cohorts { get; set; }
    }
}
