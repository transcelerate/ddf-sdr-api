using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ObjectiveEntity : SyntaxTemplateEntity
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.OBJECTIVE);
        public CodeEntity Level { get; set; }
        public List<EndpointEntity> Endpoints { get; set; }
    }
}
