﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]    
    public class StudyEpochEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyEpochId)]
        public string Id { get; set; }
        public string NextStudyEpochId { get; set; }
        public string PreviousStudyEpochId { get; set; }
        public string StudyEpochDescription { get; set; }
        public string StudyEpochName { get; set; }
        public List<CodeEntity> StudyEpochType { get; set; }               
        public List<EncounterEntity> Encounters { get; set; }
    }
}