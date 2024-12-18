using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV5;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<StudyVersionEntity> Versions { get; set; }
        public List<StudyDefinitionDocumentEntity> DocumentedBy { get; set; }        
        public string InstanceType { get; set; }
    }
}
