
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyDefinitionsEntity
    {
        public CommonStudyEntity Study { get; set; }

        public AuditTrailEntity AuditTrail { get; set; }

    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public object StudyType { get; set; }
        public object StudyPhase { get; set; }
        public List<object> StudyIdentifiers { get; set; }        
        public List<CommonStudyProtocolVersions> StudyProtocolVersions { get; set; }
        public List<CommonStudyDesigns> StudyDesigns { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyProtocolVersions
    {
        public string ProtocolVersion { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyDesigns
    {
        public string StudyDesignId { get; set; }
        public string Uuid { get; set; }
        public object InterventionModel { get; set; }
        public List<CommonStudyIndication> StudyIndications { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonCodeEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyIndication
    {
        public string IndicationDescription { get; set; }
        public string IndicationDesc { get; set; }
        public string Description { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonInvestigationalInterventionEntity
    {
        public string InterventionModel { get; set; }
    }
}
