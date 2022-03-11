using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyIndicationValidator : AbstractValidator<StudyIndicationDTO>
    {
        /// <summary>
        /// Validator for studyIndication
        /// </summary>
        public StudyIndicationValidator()
        {
            RuleFor(x => x.description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}
