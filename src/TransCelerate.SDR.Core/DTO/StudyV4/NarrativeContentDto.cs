using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class NarrativeContentDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SectionNumber { get; set; }
        public string SectionTitle { get; set; }
        public string Text { get; set; }
        public List<string> ChildIds { get; set; }
        public string PreviousId { get; set; }
        public string NextId { get; set; }
        public string InstanceType { get; set; }
    }
}
