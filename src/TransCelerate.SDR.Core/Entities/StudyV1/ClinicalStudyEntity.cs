using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class ClinicalStudyEntity : IUuid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV1.StudyId)]
        public string Uuid { get; set; }
        [MongoDB.Bson.Serialization.Attributes.BsonElement("uuid")]
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }        
        public CodeEntity StudyType { get; set; }
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public CodeEntity StudyPhase { get; set; }
        public List<StudyProtocolVersionEntity> StudyProtocolVersions { get; set; }
        public List<StudyDesignEntity> StudyDesigns { get; set; }
        public string StudyVersion { get; set; }
    }
}
