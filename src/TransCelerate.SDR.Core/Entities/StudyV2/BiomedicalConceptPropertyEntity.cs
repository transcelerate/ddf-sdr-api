using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptPropertyEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.BcPropertyId)]
        public string Id { get; set; }
        public string BcPropertyName { get; set; }
        public bool BcPropertyRequired { get; set; }
        public bool BcPropertyEnabled { get; set; }
        public string BcPropertyDataType { get; set; }
        public List<ResponseCodeEntity> BcPropertyResponseCodes { get; set; }
        public AliasCodeEntity BcPropertyConceptCode { get; set; }
    }
}
