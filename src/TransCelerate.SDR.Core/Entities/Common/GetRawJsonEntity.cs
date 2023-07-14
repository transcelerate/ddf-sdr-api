using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.Common
{
    [BsonIgnoreExtraElements]
    public class GetRawJsonEntity
    {
        public object Study { get; set; }

        public AuditTrailEntity AuditTrail { get; set; }
    }


    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime EntryDateTime { get; set; }        

        public string UsdmVersion { get; set; }

        [Newtonsoft.Json.JsonProperty(nameof(SDRUploadVersion))]
        [BsonElement(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
    }
}
