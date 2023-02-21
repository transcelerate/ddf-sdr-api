using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class TimelineDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimelineId)]
        public string Id { get; set; }
        public string TimelineDescription { get; set; }
        public ConditionDto EntryCondition { get; set; }
        public string EntryTimepointId { get; set; }
        public ExitDto TimelineExit { get; set; }
        public List<TimepointDto> TimelineTimepoints { get; set; }        
    }
}
