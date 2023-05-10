using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class InvestigationalInterventionEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.InvestigationalInterventionId)]
        public string Id { get; set; }
        public string InterventionDescription { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
