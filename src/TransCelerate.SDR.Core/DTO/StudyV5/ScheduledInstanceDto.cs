using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceDto.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceDto), nameof(Utilities.ScheduledInstanceTypeV4.ScheduledActivityInstance))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceDto), nameof(Utilities.ScheduledInstanceTypeV4.ScheduledDecisionInstance))]
    public class ScheduledInstanceDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string TimelineExitId { get; set; }
        public string TimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
