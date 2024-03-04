using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ScheduledDecisionInstanceDto : ScheduledInstanceDto
    {
        public override string ScheduledInstanceType { get; set; } = nameof(Utilities.ScheduledInstanceTypeV4.ScheduledDecisionInstance);
        public Dictionary<string, string> ConditionAssignments { get; set; }
    }
}
