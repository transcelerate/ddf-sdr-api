﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class AgentAdministrationEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public AdministrationDurationEntity Duration { get; set; }
        public QuantityEntity Dose { get; set; }
        public CodeEntity Route { get; set; }
        public CodeEntity Frequency { get; set; }
    }
}
