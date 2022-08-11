using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    [BsonIgnoreExtraElements]
    public class UserGroupMappingEntity
    {
        // public Object _id { get; set; }
        [BsonElement("SDRGroups")]
        public List<SDRGroupsEntity> SDRGroups { get; set; }
    }
}
