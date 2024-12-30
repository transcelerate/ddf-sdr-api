using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AliasCodeEntity
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.AliasCodeId)]
        public string Id { get; set; }
        public CodeEntity StandardCode { get; set; }
        public List<CodeEntity> StandardCodeAliases { get; set; }
    }
}
