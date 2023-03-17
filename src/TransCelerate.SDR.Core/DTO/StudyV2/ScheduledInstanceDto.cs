﻿using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    [JsonConverter(typeof(JsonSubtypes), nameof(ScheduledInstanceDto.ScheduledInstanceType))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledActivityInstanceDto), nameof(Utilities.ScheduledInstanceType.ACTIVITY))]
    [JsonSubtypes.KnownSubType(typeof(ScheduledDecisionInstanceDto), nameof(Utilities.ScheduledInstanceType.DECISION))]
    public class ScheduledInstanceDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ScheduledInstanceId)]
        public string Id { get; set; }
        public string ScheduleTimelineExitId { get; set; }
        public string ScheduledInstanceEncounterId { get; set; }
        public string ScheduledInstanceTimelineId { get; set; }
        public int? ScheduleSeqenceNumber { get; set; }
        public List<TimingDto> ScheduledInstanceTimings { get; set; }
        public virtual string ScheduledInstanceType { get; set; }
    }
}