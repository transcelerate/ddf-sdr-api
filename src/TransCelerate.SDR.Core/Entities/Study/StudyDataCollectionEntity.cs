using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyDataCollectionEntity
    {
        [BsonElement("id")]
        public string studyDataCollectionId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string eCRFLink { get; set; }
    }
}
