using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class AnalysisPopulationEntity : IUuid
    {
        public string Uuid { get; set; }
        public string PopulationDescription { get; set; }
    }
}
