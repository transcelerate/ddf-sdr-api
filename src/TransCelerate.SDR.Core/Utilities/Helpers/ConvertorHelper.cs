using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ConvertorHelper
    {
        public static object BsonToObjectConvertor(this BsonDocument bsonDocument)
        {
            return BsonSerializer.Deserialize<object>(bsonDocument);
        }
    }
}
