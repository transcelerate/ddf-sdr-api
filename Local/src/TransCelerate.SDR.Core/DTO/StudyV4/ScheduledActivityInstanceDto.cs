using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ScheduledActivityInstanceDto : ScheduledInstanceDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.ScheduledInstanceType.ACTIVITY);
        public string EncounterId { get; set; }
        public List<string> ActivityIds { get; set; }
    }
}
