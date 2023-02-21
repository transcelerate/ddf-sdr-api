using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class TimingEntity : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimingId)]
        public string Id { get; set; }
        public CodeEntity TimingType { get; set; }
        public string TimingValue { get; set; }
        public CodeEntity TimingRelativeToFrom { get; set; }
        public string TimingWindow { get; set; }
    }
}
