using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ActivityEntity : IId
    {        
        public string Id { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<ProcedureEntity> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public bool ActivityIsConditional { get; set; }
        public string ActivityIsConditionalReason { get; set; }
        public List<string> BcCategoryIds { get; set; }
        public List<string> BcSurrogateIds { get; set; }
        public List<string> BiomedicalConceptIds { get; set; }
        public string ActivityTimelineId { get; set; }
    }
}
