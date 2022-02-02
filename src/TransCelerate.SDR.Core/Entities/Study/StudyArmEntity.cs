using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyArmEntity
    {
        
        [BsonElement("id")]
        public string studyArmId { get; set; }
        public string description { get; set; }
        public string studyArmType { get; set; }
        public string studyOriginType { get; set; }
        public string studyArmOrigin { get; set; }
        public string name { get; set; }
    }
}
