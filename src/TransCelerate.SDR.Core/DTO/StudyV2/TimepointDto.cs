using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class TimepointDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimepointId)]
        public string Id { get; set; }
        public string TimepointDescription { get; set; }
        public CodeDto TimepointType { get; set; }
        public ConditionDto TimepointCondition { get; set; }
        public string TimepointExitId { get; set; }
        public TimingDto TimepointScheduledAt { get; set; }
        public List<string> NextTimepointIds { get; set; }
        public List<string> PreviousTimepointIds { get; set; }
        public List<string> TimepointActivityIds { get; set; }
        public string TimepointEncounterId { get; set; }
        public string EntryTimelineId { get; set; }
    }
}
