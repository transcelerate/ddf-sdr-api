using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class AgentAdministrationEntity : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public AdministrationDurationEntity Duration { get; set; }
        public QuantityEntity Dose { get; set; }
        public AliasCodeEntity Route { get; set; }
        public AliasCodeEntity Frequency { get; set; }
        public string InstanceType { get; set; }
    }
}
