using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class MatrixEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string MatrixId { get; set; }
        public List<ItemEntity> Items { get; set; }
    }
}
