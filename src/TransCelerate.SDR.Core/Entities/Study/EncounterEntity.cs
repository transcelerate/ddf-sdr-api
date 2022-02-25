using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class EncounterEntity
    {
        [BsonElement("id")]
        public string encounterId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string contactMode { get; set; }
        public string environmentalSetting { get; set; }
        public RuleEntity startRule { get; set; }
        public RuleEntity endRule { get; set; }
        public EpochEntity epoch { get; set; }
    }
}
