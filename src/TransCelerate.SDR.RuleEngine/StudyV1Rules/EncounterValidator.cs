using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for Encounter
    /// </summary>
    public class EncounterValidator : AbstractValidator<EncounterDto>
    {
        public EncounterValidator()
        {
            RuleFor(x => x.EncounterDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EncounterName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EncounterType)
                .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.EncounterEnvironmentalSetting)
                .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.EncounterContactMode)
                .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.TransitionStartRule)
                .SetValidator(new TransitionRuleValidator());

            RuleFor(x => x.TransitionEndRule)
                .SetValidator(new TransitionRuleValidator());
        }
    }
}





