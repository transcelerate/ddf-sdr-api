using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class ChangesEntity
    {
        public DateTime EntryDateTime { get; set; }

        [BsonElement(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }

        [BsonElement(nameof(SDRUploadFlag))]
        public int SDRUploadFlag { get; set; }

        public List<string> Elements { get; set; }
    }
}
