using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchParametersDTO
    {
        [RegularExpression(Constants.RegularExpressions.AlphaNumericsWithSpace, ErrorMessage = Constants.ValidationErrorMessage.AlphaNumericErrorMessage)]
        public string studyId { get; set; }

        [RegularExpression(Constants.RegularExpressions.AlphaNumericsWithSpace, ErrorMessage = Constants.ValidationErrorMessage.AlphaNumericErrorMessage)]
        public string studyTitle { get; set; }

        [RegularExpression(Constants.RegularExpressions.AlphaNumericsWithSpace, ErrorMessage = Constants.ValidationErrorMessage.AlphaNumericErrorMessage)]
        public string briefTitle { get; set; }

        [RegularExpression(Constants.RegularExpressions.AlphaNumericsWithSpace, ErrorMessage = Constants.ValidationErrorMessage.AlphaNumericErrorMessage)]
        public string indication { get; set; }
        public string interventionModel  { get; set; }
        public string phase { get; set; }

        [DateValidationHelper(ErrorMessage = "Please enter a valid date")]        
        public string fromDate { get; set; }

        [DateValidationHelper(ErrorMessage = "Please enter a valid date")]
        public string toDate { get; set; }
      
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid page size")]
        public int pageSize { get; set; }
               
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid page number")]
        public int pageNumber { get; set; }
        public string header { get; set; }
        public bool asc { get; set; }

        public SearchParametersDTO()
        {
            pageNumber = 1;
            pageSize = 5;
        }
    }

}
