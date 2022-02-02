using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyElementEntity
    {
        [BsonElement("id")]
        public string studyElementId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public RuleEntity startRule { get; set; }
        public RuleEntity endRule { get; set; }
    }
}
