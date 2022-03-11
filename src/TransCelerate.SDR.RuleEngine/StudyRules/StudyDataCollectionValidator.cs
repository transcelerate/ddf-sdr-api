using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyDataCollectionValidator : AbstractValidator<StudyDataCollectionDTO>
    {
        /// <summary>
        /// Validator for StudyDataCollection
        /// </summary>
        public StudyDataCollectionValidator()
        {
            RuleFor(x => x.name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);       
        }
    }
}
