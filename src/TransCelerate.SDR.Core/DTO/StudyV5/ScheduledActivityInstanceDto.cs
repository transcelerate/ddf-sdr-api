using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ScheduledActivityInstanceDto : ScheduledInstanceDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.ACTIVITY);
        public string TimelineExitId { get; set; }
        public string TimelineId { get; set; }
        public string EncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
