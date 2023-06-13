using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ScheduledActivityInstanceDto : ScheduledInstanceDto
    {
        public override string ScheduledInstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.ACTIVITY);
        public string ScheduledActivityInstanceEncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
