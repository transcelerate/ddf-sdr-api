using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ActivityEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ActivityId)]        
        public string Id { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<ProcedureEntity> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public List<StudyDataEntity> StudyDataCollection { get; set; }
        public bool ActivityIsOptional { get; set; }
        public string ActivityIsOptionalReason { get; set; }
    }
}
