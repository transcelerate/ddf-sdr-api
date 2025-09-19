using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AddressEntity : IId
    {        
        public string Id { get; set; }
        public string Text { get; set; }
        public List<string> Lines { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public CodeEntity Country { get; set; }
        public string InstanceType { get; set; }
    }
}
