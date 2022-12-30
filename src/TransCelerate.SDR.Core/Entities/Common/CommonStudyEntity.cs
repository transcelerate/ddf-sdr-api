
namespace TransCelerate.SDR.Core.Entities.Common
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyEntity
    {
        public CommonClinicalStudyEntity ClinicalStudy { get; set; }

        public AuditTrailEntity AuditTrail { get; set; }

    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonClinicalStudyEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public object StudyType { get; set; }
        public object StudyIdentifiers { get; set; }
        public object StudyProtocolVersions { get; set; }
    }
}
