using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class MatrixEntity
    {
        [BsonElement("id")]
        public string matrixId { get; set; }
        public List<ItemEntity> items { get; set; }
    }
}
