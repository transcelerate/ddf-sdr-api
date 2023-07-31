﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyCellEntity : IId
    {        
        public string Id { get; set; }
        public string StudyArmId { get; set; }
        public string StudyEpochId { get; set; }
        public List<string> StudyElementIds { get; set; }
    }
}
