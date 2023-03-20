using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class IndicationEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.IndicationId)]
        public string Id { get; set; }
        public string IndicationDescription { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
