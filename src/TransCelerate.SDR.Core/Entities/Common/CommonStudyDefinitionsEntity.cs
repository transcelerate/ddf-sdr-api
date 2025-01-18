﻿
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
        public object StudyTitle { get; set; }
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
	public class CommonStudyDefinitionsEntityV5
	{
		public CommonStudyEntityV5 Study { get; set; }

		public AuditTrailEntity AuditTrail { get; set; }

	}
	[MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
	[MongoDB.Bson.Serialization.Attributes.BsonNoId]
	public class CommonStudyEntityV5
	{
		public string StudyId { get; set; }
		public string Id { get; set; }
		public object StudyTitle { get; set; }
		public string StudyVersion { get; set; }
		public object StudyType { get; set; }
		public object Type { get; set; }
		public object StudyPhase { get; set; }
		public List<object> StudyIdentifiers { get; set; }
		public List<CommonStudyProtocolVersions> StudyProtocolVersions { get; set; }
		public List<CommonStudyDesigns> StudyDesigns { get; set; }
		public List<CommonStudyVersions> Versions { get; set; }
		public List<CommonStudyDocumentedBy> DocumentedBy { get; set; }
	}

	[MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyProtocolVersions
    {
        public string ProtocolVersion { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class CommonStudyDocumentedBy
    {
        public List<CommonStudyProtocolVersions> Versions { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyVersions
    {
        public string Id { get; set; }
        public object Titles {  get; set; }        
        public string VersionIdentifier { get; set; }        
        public object StudyType { get; set; }
        public object StudyPhase { get; set; }
        public List<object> StudyIdentifiers { get; set; }        
        public List<CommonStudyDesignsV4> StudyDesigns { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyTitle
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public CommonCodeEntity Type { get; set; }
        public string InstanceType { get; set; }
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
        public List<CommonStudyIndication> Indications { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonCodeEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class CommonStudyIndication
    {
        public string IndicationDescription { get; set; }
        public string IndicationDesc { get; set; }
        public string Description { get; set; }
    }
}
