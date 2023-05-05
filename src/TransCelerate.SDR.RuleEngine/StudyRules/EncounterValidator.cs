﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class EncounterValidator : AbstractValidator<EncounterDTO>
    {
        /// <summary>
        /// Validator for Encounter
        /// </summary>
        public EncounterValidator()
        {
            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EndRule)
                .SetValidator(new RuleValidator());

            RuleFor(x => x.StartRule)
                .SetValidator(new RuleValidator());

            RuleFor(x => x.Epoch)
                .SetValidator(new EpochValidator());
        }
    }
}
