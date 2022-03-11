using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyElementsValidator : AbstractValidator<StudyElementDTO>
    {
        public StudyElementsValidator()
        {           
            //RuleFor(x => x.name)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            //RuleFor(x => x.description)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            //RuleFor(x => x.startRule)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            //RuleFor(x => x.endRule)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}
