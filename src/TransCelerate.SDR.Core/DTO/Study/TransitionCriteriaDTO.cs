using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class TransitionCriteriaDTO
    {
        public string id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public List<string> specificationIds { get; set; }
        public string domain { get; set; }
        public string dictionary { get; set; }
        public string testName { get; set; }
        public string numericMinValue { get; set; }
        public string numericMinValueInclusive { get; set; }
        public string numericMaxValue { get; set; }
        public string valueUnit { get; set; }
        public string textualResult { get; set; }
        public string timing { get; set; }
        public string timingDetail { get; set; }
        public string timingGap { get; set; }
        public int timingDays { get; set; }
        public string route { get; set; }
        public string numericaMaxValueInclusive { get; set; }
        public List<string> criterionDetails { get; set; }
    }
}
