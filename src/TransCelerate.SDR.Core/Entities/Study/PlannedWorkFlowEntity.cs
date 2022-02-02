using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class PlannedWorkFlowEntity
    {
        [BsonElement("id")]
        public string plannedWorkFlowId { get; set; }
        public string description { get; set; }
        public PointInTimeEntity startPoint { get; set; }
        public PointInTimeEntity endPoint { get; set; }
        public List<TransitionEntity> transitions { get; set; }
    }
}
