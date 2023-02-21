using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class TimepointEntity : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimepointId)]
        public string Id { get; set; }
        public string TimepointDescription { get; set; }
        public CodeEntity TimepointType { get; set; }
        public ConditionEntity TimepointCondition { get; set; }
        public string TimepointExitId { get; set; }
        public TimingEntity TimepointScheduledAt { get; set; }
        public List<string> NextTimepointIds { get; set; }
        public List<string> PreviousTimepointIds { get; set; }
        public List<string> TimepointActivityIds { get; set; }
        public string TimepointEncounterId { get; set; }
        public string EntryTimelineId { get; set; }
    }
}
