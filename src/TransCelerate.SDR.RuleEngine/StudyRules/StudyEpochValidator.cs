using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyEpochValidator : AbstractValidator<StudyEpochDTO>
    {
        /// <summary>
        /// Validator for StudyEpoch
        /// </summary>
        public StudyEpochValidator()
        {

            RuleFor(x => x.EpochType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}
