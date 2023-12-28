using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class RangeDto : IId
    {
        public string Id { get; set; }        
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public CodeDto Unit { get; set; }
        public bool IsApproximate { get; set; }
    }
}
