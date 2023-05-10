using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ScheduledDecisionInstanceDto : ScheduledInstanceDto
    {
        public override string ScheduledInstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.DECISION);
        public Dictionary<string, string> ConditionAssignments { get; set; }
    }
}
