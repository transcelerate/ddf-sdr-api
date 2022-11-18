using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for StudyCells
    /// </summary>
    public class StudyCellsValidator : AbstractValidator<StudyCellDto>
    {
        public StudyCellsValidator()
        {
            RuleFor(x => x.Id)
              .Cascade(CascadeMode.Stop)
              .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyElements)
              .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

        }
    }
}





