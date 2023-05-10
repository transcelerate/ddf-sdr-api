using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptCategoryEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.BiomedicalConceptCategoryId)]
        public string Id { get; set; }
        public List<string> BcCategoryParentIds { get; set; }
        public List<string> BcCategoryChildrenIds { get; set; }
        public string BcCategoryName { get; set; }
        public string BcCategoryDescription { get; set; }
        public List<string> BcCategoryMemberIds { get; set; }
    }
}
