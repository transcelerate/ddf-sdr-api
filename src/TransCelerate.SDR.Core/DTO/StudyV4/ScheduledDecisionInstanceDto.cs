using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ScheduledDecisionInstanceDto : ScheduledInstanceDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.DECISION);
        public List<ConditionAssignmentDto> ConditionAssignments { get; set; }
    }
}
