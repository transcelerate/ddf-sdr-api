using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class TimelineEntity : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimelineId)]
        public string Id { get; set; }
        public string TimelineDescription { get; set; }
        public ConditionEntity EntryCondition { get; set; }
        public string EntryTimepointId { get; set; }
        public ExitEntity TimelineExit { get; set; }
        public List<TimepointEntity> TimelineTimepoints { get; set; }        
    }
}
