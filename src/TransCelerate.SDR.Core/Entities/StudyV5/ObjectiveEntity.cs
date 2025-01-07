using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ObjectiveEntity : SyntaxTemplateEntity
    {        
        public CodeEntity Level { get; set; }
        public List<EndpointEntity> Endpoints { get; set; }
    }
}
