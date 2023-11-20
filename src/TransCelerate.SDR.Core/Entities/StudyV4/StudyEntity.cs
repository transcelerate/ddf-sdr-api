﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyEntity
    {
        public string Id { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public CodeEntity StudyType { get; set; }
        public string StudyRationale { get; set; }
        public string StudyAcronym { get; set; }
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public AliasCodeEntity StudyPhase { get; set; }
        public List<CodeEntity> BusinessTherapeuticAreas { get; set; }
        public List<StudyProtocolVersionEntity> StudyProtocolVersions { get; set; }
        public List<StudyDesignEntity> StudyDesigns { get; set; }

    }
}
