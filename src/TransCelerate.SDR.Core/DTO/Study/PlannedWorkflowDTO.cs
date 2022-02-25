using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class PlannedWorkflowDTO
    {
        
        public string id { get; set; }
       
        public string description { get; set; }
               
        public PointInTimeDTO startPoint { get; set; }
       
        public PointInTimeDTO endPoint { get; set; }
               
        //public List<TransitionDTO> transitions { get; set; }

        public WorkflowItemMatrixDTO workflowItemMatrix { get; set; }
    }
}
