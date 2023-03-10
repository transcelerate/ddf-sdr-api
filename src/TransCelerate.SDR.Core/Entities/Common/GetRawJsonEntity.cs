using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Common
{
    [BsonIgnoreExtraElements]
    public class GetRawJsonEntity
    {
        public object ClinicalStudy { get; set; }

        public AuditTrailEntity AuditTrail { get; set; }
    }


    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string CreatedBy { get; set; }

        public string UsdmVersion { get; set; }

        [Newtonsoft.Json.JsonProperty("SDRUploadVersion")]
        [BsonElement("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
    }
}
