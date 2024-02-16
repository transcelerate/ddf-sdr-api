using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{    
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class SyntaxTemplateEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string DictionaryId { get; set; }        
        public string InstanceType { get; set; }
    }
}
