using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UsdmVersion { get; set; }
        [BsonElement(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
    }
}
