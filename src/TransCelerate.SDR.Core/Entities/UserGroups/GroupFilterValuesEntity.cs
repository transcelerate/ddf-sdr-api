using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public  class GroupFilterValuesEntity
    {
        [BsonElement("id")]
        public string groupFilterValueId { get; set; }

        public string title { get; set; }
    }
}
