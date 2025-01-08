using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDesignPopulationEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDesignPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
        public int PlannedNumberOfParticipants { get; set; }
        public string PlannedMaximumAgeOfParticipants { get; set; }
        public string PlannedMinimumAgeOfParticipants { get; set; }
        public List<CodeEntity> PlannedSexOfParticipants { get; set; }
    }
}
