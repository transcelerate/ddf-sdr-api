using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Common
{
    [BsonIgnoreExtraElements]
    public class GetRawJsonEntity
    {
        public Object _id { get; set; }

        public object ClinicalStudy { get; set; }

        public AuditTrailEntity AuditTrail { get; set; }
    }


    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string CreatedBy { get; set; }

        [BsonElement("usdm-version")]
        public string UsdmVersion { get; set; }
        [BsonElement("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
    }
}
