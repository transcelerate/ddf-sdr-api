using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class ClinicalStudyEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyType { get; set; }

        public string StudyPhase { get; set; }
        public string StudyStatus { get; set; }
        public string StudyTag { get; set; }

        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }

        public List<CurrentSectionsEntity> CurrentSections { get; set; }

        public List<StudyProtocolEntity> StudyProtocolReferences { get; set; }

    }
}
