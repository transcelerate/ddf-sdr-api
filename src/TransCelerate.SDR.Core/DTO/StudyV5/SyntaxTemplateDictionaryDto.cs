﻿using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{        
    public class SyntaxTemplateDictionaryDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        //public string Text { get; set; }
        public List<ParameterMapDto> ParameterMaps { get; set; }
        public string InstanceType { get; set; }
    }
}
