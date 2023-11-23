using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptPropertyEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
        public bool Enabled { get; set; }
        public string DataType { get; set; }
        public List<ResponseCodeEntity> ResponseCodes { get; set; }
        public AliasCodeEntity Code { get; set; }
    }
}
