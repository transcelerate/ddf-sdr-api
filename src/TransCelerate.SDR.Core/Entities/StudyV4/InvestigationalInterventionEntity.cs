using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class InvestigationalInterventionEntity : IId
    {        
        public string Id { get; set; }
        public string InterventionDescription { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
