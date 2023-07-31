using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AliasCodeEntity
    {        
        public string Id { get; set; }
        public CodeEntity StandardCode { get; set; }
        public List<CodeEntity> StandardCodeAliases { get; set; }
    }
}
