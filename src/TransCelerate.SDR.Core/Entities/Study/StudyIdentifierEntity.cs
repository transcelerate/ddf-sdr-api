using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyIdentifierEntity
    {
        [BsonElement("id")]
        public string studyIdentifierId { get; set; }
        public string orgCode { get; set; }
        public string name { get; set; }
        public string idType { get; set; }

    }
}
