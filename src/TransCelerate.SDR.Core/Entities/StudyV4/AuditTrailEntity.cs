using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UsdmVersion { get; set; }
        [BsonElement(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        //*********** Added By Basha for US#441
        [BsonElement(nameof(SDRUploadFlag))]
        public int SDRUploadFlag { get; set; }
    }
}
