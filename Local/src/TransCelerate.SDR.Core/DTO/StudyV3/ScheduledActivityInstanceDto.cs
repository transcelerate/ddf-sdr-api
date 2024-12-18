using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ScheduledActivityInstanceDto : ScheduledInstanceDto
    {
        public override string ScheduledInstanceType { get; set; } = nameof(Utilities.ScheduledInstanceTypeV4.ScheduledActivityInstance);
        public string ScheduledActivityInstanceEncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
