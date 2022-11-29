using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Encounter
    /// </summary>
    public class EncounterValidator : AbstractValidator<EncounterDto>
    {
        public EncounterValidator()
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.EncounterId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.EncounterId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EncounterDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EncounterName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EncounterContactMode)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
            
            RuleFor(x => x.EncounterEnvironmentalSetting)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
            
            RuleFor(x => x.EncounterType)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}





