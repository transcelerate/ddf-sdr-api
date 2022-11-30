using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class AuditTrailEntity
    {
        public DateTime entryDateTime { get; set; }
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
    }
}
