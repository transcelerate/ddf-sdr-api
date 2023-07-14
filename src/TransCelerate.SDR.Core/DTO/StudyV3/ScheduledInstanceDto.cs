using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceDto.ScheduledInstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceDto), nameof(Utilities.ScheduledInstanceType.ACTIVITY))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceDto), nameof(Utilities.ScheduledInstanceType.DECISION))]
    public class ScheduledInstanceDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.ScheduledInstanceId)]
        public string Id { get; set; }
        public string ScheduleTimelineExitId { get; set; }
        public string ScheduledInstanceTimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public List<TimingDto> ScheduledInstanceTimings { get; set; }
        public virtual string ScheduledInstanceType { get; set; }
    }
}
