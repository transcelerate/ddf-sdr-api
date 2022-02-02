using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class CodingEntity
    {
        public string code { get; set; }
        public string codeSystem { get; set; }
        public string codeSystemVersion { get; set; }
        public string decode { get; set; }
    }
}
