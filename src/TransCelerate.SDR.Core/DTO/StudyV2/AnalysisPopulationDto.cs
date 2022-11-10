using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class AnalysisPopulationDto : IUuid
    {
        public string Uuid { get; set; }
        public string PopulationDesc { get; set; }
    }
}
