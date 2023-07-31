using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ObjectiveEntity : IId
    {        
        public string Id { get; set; }
        public string ObjectiveDescription { get; set; }
        public CodeEntity ObjectiveLevel { get; set; }
        public List<EndpointEntity> ObjectiveEndpoints { get; set; }
    }
}
