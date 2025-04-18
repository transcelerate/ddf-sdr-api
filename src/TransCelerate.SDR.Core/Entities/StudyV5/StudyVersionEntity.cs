﻿using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV5;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyVersionEntity : IId
    {
        public string Id { get; set; }
        public List<StudyTitleEntity> Titles { get; set; }
        public string VersionIdentifier { get; set; }
        public CodeEntity StudyType { get; set; }
        public string Rationale { get; set; }
		public List<string> DocumentVersionIds { get; set; }
		public List<GovernanceDateEntity> DateValues { get; set; }
        public List<StudyAmendmentEntity> Amendments { get; set; }        
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public AliasCodeEntity StudyPhase { get; set; }
        public List<CodeEntity> BusinessTherapeuticAreas { get; set; }        
        public List<StudyDesignEntity> StudyDesigns { get; set; }
        public string InstanceType { get; set; }
		public List<CommentAnnotationEntity> Notes { get; set; }
		public List<EligibilityCriterionEntity> Criteria { get; set; }
		public List<NarrativeContentItemEntity> NarrativeContentItems { get; set; }
		public List<AbbreviationEntity> Abbreviations { get; set; }
	}
}
