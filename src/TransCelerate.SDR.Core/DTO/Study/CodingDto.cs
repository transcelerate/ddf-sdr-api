using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class CodingDTO
    {
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string code { get; set; }
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string codeSystem { get; set; }
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string codeSystemVersion { get; set; }
        [Required(ErrorMessage = Constants.ValidationErrorMessage.ConformanceError)]
        public string decode { get; set; }
    }
}
