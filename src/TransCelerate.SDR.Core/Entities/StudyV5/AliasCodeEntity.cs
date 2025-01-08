using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AliasCodeEntity : IId
    {        
        public string Id { get; set; }
        public CodeEntity StandardCode { get; set; }
        public List<CodeEntity> StandardCodeAliases { get; set; }
        public string InstanceType { get; set; }
    }
}
