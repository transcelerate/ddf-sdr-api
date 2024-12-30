using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class IndicationEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.IndicationId)]
        public string Id { get; set; }
        public string IndicationDescription { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
