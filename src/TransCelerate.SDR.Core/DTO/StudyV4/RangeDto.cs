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
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public CodeDto Unit { get; set; }
        public object IsApproximate { get; set; }
        public string InstanceType { get; set; }
    }
}
