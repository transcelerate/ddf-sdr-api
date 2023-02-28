using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    [BsonDiscriminator(nameof(ScheduledInstanceEntity.ScheduleInstanceType))]
    [BsonKnownTypes(typeof(ScheduledActivityInstanceEntity))]
    [BsonKnownTypes(typeof(ScheduledDecisionInstanceEntity))]
    public class ScheduledInstanceEntity : Iid
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ScheduledInstanceId)]
        public string Id { get; set; }
        public string ScheduledInstanceEncounterId { get; set; }
        public string ScheduleTimelineExitId { get; set; }
        public string ScheduledInstanceTimelineId { get; set; }
        public int ScheduleSeqenceNumber { get; set; }
        public List<TimingEntity> ScheduledInstanceTimings { get; set; }
        public virtual string ScheduleInstanceType { get; set; }
    }
}
