﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class GovernanceDateDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeDto Type { get; set; }
        public string DateValue { get; set; }
        public List<GeographicScopeDto> GeographicScopes {  get; set; }
        public string InstanceType { get; set; }
    }
}
