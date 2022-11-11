using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class ChangesEntity
    {
        public DateTime EntryDateTime { get; set; }

        [BsonElement("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }

        public List<string> Elements { get; set; }
    }
}
