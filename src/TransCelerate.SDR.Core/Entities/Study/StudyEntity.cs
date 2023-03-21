using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyEntity
    {
        public object Id { get; set; }
        public ClinicalStudyEntity ClinicalStudy { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }



}
