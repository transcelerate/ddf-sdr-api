using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ScheduledActivityInstanceDto : ScheduledInstanceDto
    {
        public override string ScheduledInstanceType { get; set; } = TransCelerate.SDR.Core.Utilities.ScheduledInstanceType.ACTIVITY.ToString();
        public List<string> ActivityIds { get; set; }
    }
}
