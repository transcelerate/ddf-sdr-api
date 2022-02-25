using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyElementDTO
    {
    
        public string id { get; set; }
      
        public string name { get; set; }
      
        public string description { get; set; }
      
        public RuleDTO startRule { get; set; }
      
        public RuleDTO endRule { get; set; }
    }
}
