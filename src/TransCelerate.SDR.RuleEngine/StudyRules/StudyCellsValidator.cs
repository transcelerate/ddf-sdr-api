using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyCellsValidator : AbstractValidator<StudyCellDTO>
    {
        /// <summary>
        /// Validator for StudyCells
        /// </summary>
        public StudyCellsValidator()
        {
            RuleFor(x => x.StudyArm)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .SetValidator(new StudyArmValidator());
            RuleFor(x => x.StudyEpoch)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .SetValidator(new StudyEpochValidator());

            RuleFor(x => x.StudyElements)
                .ForEach(x => x.SetValidator(new StudyElementsValidator()));
        }
    }
}
