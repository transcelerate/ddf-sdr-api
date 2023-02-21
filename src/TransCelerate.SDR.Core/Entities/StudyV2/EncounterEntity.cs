﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EncounterEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.EncounterId)]
        public string Id { get; set; }
        public List<CodeEntity> EncounterContactModes { get; set; }
        public string EncounterDescription { get; set; }
        public List<CodeEntity> EncounterEnvironmentalSetting { get; set; }
        public string EncounterName { get; set; }
        public List<CodeEntity> EncounterType { get; set; }
        public TransitionRuleEntity TransitionStartRule { get; set; }
        public TransitionRuleEntity TransitionEndRule { get; set; }
        public string NextEncounterId { get; set; }
        public string PreviousEncounterId { get; set; }
        public string EncounterScheduledAt { get; set; }
    }
}
