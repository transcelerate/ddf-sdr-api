using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class TransitionRuleDTO
    {
        public string id { get; set; }
        public string description { get; set; }
        public List<CodingDTO> coding { get; set; }
    }
}
