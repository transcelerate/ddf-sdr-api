using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class EncounterDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string contactMode { get; set; }
        public string environmentalSetting { get; set; }
        public RuleDTO startRule { get; set; }
        public RuleDTO endRule { get; set; }
        public EpochDTO epoch { get; set; }
    }
}
