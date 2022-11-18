﻿using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ClinicalStudyEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public CodeEntity StudyType { get; set; }
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public CodeEntity StudyPhase { get; set; }
        public List<CodeEntity> BusinessTherapeuticAreas { get; set; }
        public List<StudyProtocolVersionEntity> StudyProtocolVersions { get; set; }
        public List<StudyDesignEntity> StudyDesigns { get; set; }
        
    }
}
