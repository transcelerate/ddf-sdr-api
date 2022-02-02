using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public  class TransitionEntity
    {
        [BsonElement("id")]
        public string transitionId { get; set; }
        public string description { get; set; }
        public PointInTimeEntity fromPointInTime { get; set; }
        public PointInTimeEntity toPointInTime { get; set; }
        public TransitionRuleEntity transitionRule { get; set; }
        public string describedBy { get; set; }
        public List<TransitionCriteriaEntity> transitionCriteria { get; set; }
        public int studyProtocolCriterionTransitionNumber { get; set; }
    }
}
