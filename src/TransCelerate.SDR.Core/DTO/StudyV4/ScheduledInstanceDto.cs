using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceDto.InstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceDto), nameof(Utilities.ScheduledInstanceType.ACTIVITY))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceDto), nameof(Utilities.ScheduledInstanceType.DECISION))]
    public class ScheduledInstanceDto : IId
    {        
        public string Id { get; set; }
        public string TimelineExitId { get; set; }
        public string TimelineId { get; set; }
        public string DefaultConditionId { get; set; }
        public string EpochId { get; set; }
        public virtual string InstanceType { get; set; }
    }
}
