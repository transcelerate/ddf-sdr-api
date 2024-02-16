using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class QuantityEntity : IId
    {
        public string Id { get; set; }
        public CodeEntity Unit { get; set; }
        public int Value { get; set; }
        public string InstanceType { get; set; }
    }
}
