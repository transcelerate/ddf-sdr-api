﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class QuantityDto : IId
    {
        public string Id { get; set; }
        public AliasCodeDto Unit { get; set; }
        public object Value { get; set; }
        public string InstanceType { get; set; }
    }
}
