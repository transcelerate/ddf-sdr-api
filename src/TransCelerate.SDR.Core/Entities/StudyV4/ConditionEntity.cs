using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ConditionEntity : SyntaxTemplateEntity
    {                
        public List<string> ContextIds { get; set; }
        public List<string> AppliesToIds { get; set; }
    }
}
