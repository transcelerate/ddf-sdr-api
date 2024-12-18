using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class GovernanceDateEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeEntity Type { get; set; }
        public string DateValue { get; set; }
        public List<GeographicScopeEntity> GeographicScopes {  get; set; }
        public string InstanceType { get; set; }
    }
}
