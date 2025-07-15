using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class PersonNameEntity : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public string FamilyName { get; set; }
        public List<string> GivenNames { get; set; }
        public List<string> Prefixes { get; set; }
        public List<string> Suffixes { get; set; }
    }
}
