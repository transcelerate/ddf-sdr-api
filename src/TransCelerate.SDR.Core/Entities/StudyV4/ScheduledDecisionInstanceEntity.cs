﻿using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV4;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduledDecisionInstanceEntity : ScheduledInstanceEntity
    {
        public override string InstanceType { get; set; } = nameof(Utilities.ScheduledInstanceTypeV4.ScheduledDecisionInstance);
        public List<ConditionAssignmentEntity> ConditionAssignments { get; set; }
    }
}
