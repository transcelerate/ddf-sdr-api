using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class ActivityEntity
    {
        [BsonElement("id")]
        public string activityId { get; set; }
        public string description { get; set; }
        public List<DefinedProcedureEntity> definedProcedures { get; set; }
        public List<StudyDataCollectionEntity> studyDataCollection { get; set; }
    }
}
