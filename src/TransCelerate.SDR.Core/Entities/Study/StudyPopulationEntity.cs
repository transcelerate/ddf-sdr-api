using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;


namespace TransCelerate.SDR.Core.Entities.Study
{
   
    [BsonIgnoreExtraElements]
    public class StudyPopulationEntity
    {
        [BsonElement("Id")]
        public string studyPopulationId { get; set; }
        public string description { get; set; }
    }
}
