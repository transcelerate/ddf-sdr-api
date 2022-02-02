using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
   
    [BsonIgnoreExtraElements]
    public class StudyProtocolEntity
    {
        public List<AmendmentEntity> amendments { get; set; }
        public string protocolId { get; set; }
        public string briefTitle { get; set; }
        public string officialTitle { get; set; }
        public string publicTitle { get; set; }
        public string version { get; set; }
        public List<string> sections { get; set; }
    }
}
