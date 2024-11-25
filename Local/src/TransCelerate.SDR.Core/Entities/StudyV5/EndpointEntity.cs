using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class EndpointEntity : SyntaxTemplateEntity
    {        
        public string Purpose { get; set; }
        public CodeEntity Level { get; set; }		
	}
}
