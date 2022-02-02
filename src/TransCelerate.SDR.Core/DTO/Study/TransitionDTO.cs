using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class TransitionDTO
    {
        public string id { get; set; }
        public string description { get; set; }
        public PointInTimeDTO fromPointInTime { get; set; }
        public PointInTimeDTO toPointInTime { get; set; }
        public TransitionRuleDTO transitionRule { get; set; }
        public string describedBy { get; set; }
        public List<TransitionCriteriaDTO> transitionCriteria { get; set; }
        public int studyProtocolCriterionTransitionNumber { get; set; }
    }
}
