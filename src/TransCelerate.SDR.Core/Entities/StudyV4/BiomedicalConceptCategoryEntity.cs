using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptCategoryEntity : IId
    {        
        public string Id { get; set; }        
        public List<string> BcCategoryChildIds { get; set; }
        public string BcCategoryName { get; set; }
        public string BcCategoryDescription { get; set; }
        public List<string> BcCategoryMemberIds { get; set; }
        public AliasCodeEntity BcCategoryCode { get; set; }
    }
}
