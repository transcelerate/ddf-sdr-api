using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class TimingDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimingId)]
        public string Id { get; set; }
        public CodeDto TimingType { get; set; }
        public string TimingValue { get; set; }
        public CodeDto TimingRelativeToFrom { get; set; }
        public string TimingWindow { get; set; }
    }
}
