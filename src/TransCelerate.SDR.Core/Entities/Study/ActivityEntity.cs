using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class ActivityEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string ActivityId { get; set; }
        public string Description { get; set; }
        public List<DefinedProcedureEntity> DefinedProcedures { get; set; }
        public List<StudyDataCollectionEntity> StudyDataCollection { get; set; }
    }
}
