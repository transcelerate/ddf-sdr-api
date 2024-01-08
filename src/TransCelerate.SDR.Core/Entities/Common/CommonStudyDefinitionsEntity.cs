
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
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyEntity
    {
        public string StudyId { get; set; }
        public string Id { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public object StudyType { get; set; }
        public object Type { get; set; }
        public object StudyPhase { get; set; }
        public List<object> StudyIdentifiers { get; set; }        
        public List<CommonStudyProtocolVersions> StudyProtocolVersions { get; set; }
        public List<CommonStudyDesigns> StudyDesigns { get; set; }
        public List<CommonStudyVersions> Versions { get; set; }
        public CommonStudyDocumentedBy DocumentedBy { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyProtocolVersions
    {
        public string ProtocolVersion { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyDocumentedBy
    {
        public List<CommonStudyProtocolVersions> Versions { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyVersions
    {
        public string Id { get; set; }
        public string StudyTitle { get; set; }
        public string VersionIdentifier { get; set; }        
        public object Type { get; set; }
        public object StudyPhase { get; set; }
        public List<object> StudyIdentifiers { get; set; }        
        public List<CommonStudyDesignsV4> StudyDesigns { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyDesigns
    {
        public string Id { get; set; }
        public string StudyDesignId { get; set; }
        public string Uuid { get; set; }
        public object InterventionModel { get; set; }
        public List<CommonStudyIndication> StudyIndications { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyDesignsV4
    {
        public string Id { get; set; }
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
