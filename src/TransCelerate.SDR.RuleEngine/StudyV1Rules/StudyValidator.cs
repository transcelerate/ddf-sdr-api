using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngineV1
{
    public class StudyValidator : AbstractValidator<StudyDto>
    {
        /// <summary>
        /// Validator for SearchParameters
        /// </summary>
        public StudyValidator()
        {
            RuleFor(x => x.ClinicalStudy)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.RootElementMissing);
        }
    }
}
