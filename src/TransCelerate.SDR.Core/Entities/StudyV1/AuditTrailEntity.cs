using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string CreatedBy { get; set; }
        [BsonElement("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
    }
}
