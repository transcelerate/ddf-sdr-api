using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class EpochEntity
    {
        [BsonElement("id")]
        public string epochId { get; set; }
        public string epochType { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int sequenceInStudy { get; set; }
    }
}
