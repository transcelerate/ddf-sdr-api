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

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string description { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]        
        public PointInTimeDTO startPoint { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public PointInTimeDTO endPoint { get; set; }

        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        [EmptyListValidationHelper(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public List<TransitionDTO> transitions { get; set; }
    }
}
