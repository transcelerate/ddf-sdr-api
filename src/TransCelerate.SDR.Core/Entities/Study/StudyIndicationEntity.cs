using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{

    [BsonIgnoreExtraElements]
    public class StudyIndicationEntity
    {     
        [BsonElement("id")]     
        public string studyIndicationId{ get; set; }

        public string description { get; set; }

        public List<CodingEntity> coding { get; set; }
    }
}
