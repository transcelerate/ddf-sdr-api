using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string EntrySystem { get; set; }
        [BsonElement(nameof(Entities.Common.AuditTrailEntity.SDRUploadVersion))]
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
