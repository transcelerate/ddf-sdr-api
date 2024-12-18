using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.UserGroups
{
    public class GroupFilterValuesEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string GroupFilterValueId { get; set; }

        public string Title { get; set; }
    }
}
