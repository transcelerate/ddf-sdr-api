using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    [BsonIgnoreExtraElements]
    public class UserGroupMappingEntity
    {
        [BsonElement(nameof(SDRGroups))]
        public List<SDRGroupsEntity> SDRGroups { get; set; }
    }
}
