using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class EpochValidator : AbstractValidator<EpochDTO>
    {
        public EpochValidator()
        {
            //RuleFor(x => x.description)
            //   .Cascade(CascadeMode.Stop)
            //   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            //RuleFor(x => x.name)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.epochType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            //RuleFor(x => x.sequenceInStudy)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            //    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

        }
    }
}
