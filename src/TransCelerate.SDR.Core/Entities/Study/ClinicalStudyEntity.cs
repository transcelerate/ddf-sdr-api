using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class ClinicalStudyEntity
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }    
        public string studyType { get; set; }        

        public string studyPhase { get; set; }
        public string studyStatus { get; set; }
        public string studyTag { get; set; }

        public List<StudyIdentifierEntity> studyIdentifiers { get; set; }

        public List<CurrentSectionsEntity> currentSections { get; set; }       

    }
}
