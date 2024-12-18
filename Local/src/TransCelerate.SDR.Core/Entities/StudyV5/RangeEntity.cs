using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class RangeEntity : IId
    {
        public string Id { get; set; }        
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public CodeEntity Unit { get; set; }
        public bool IsApproximate { get; set; }
        public string InstanceType { get; set; }
    }
}
