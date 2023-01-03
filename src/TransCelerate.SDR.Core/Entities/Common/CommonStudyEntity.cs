
using System.Collections.Generic;

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
        public string StudyVersion { get; set; }
        public object StudyType { get; set; }
        public object StudyIdentifiers { get; set; }
        public List<CommonStudyProtocolVersions> StudyProtocolVersions { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyProtocolVersions
    {
        public string ProtocolVersion { get; set; }
    }
}
