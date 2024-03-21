using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class AgentAdministrationDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public AdministrationDurationDto Duration { get; set; }
        public QuantityDto Dose { get; set; }
        public CodeDto Route { get; set; }
        public CodeDto Frequency { get; set; }
        public string InstanceType { get; set; }
    }
}
