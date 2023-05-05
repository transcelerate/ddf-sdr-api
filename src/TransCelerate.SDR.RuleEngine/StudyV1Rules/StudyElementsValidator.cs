using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyElements
    /// </summary>
    public class StudyElementsValidator : AbstractValidator<StudyElementDto>
    {
        public StudyElementsValidator()
        {
            RuleFor(x => x.StudyElementDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyElementName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.TransitionStartRule)
                .SetValidator(new TransitionRuleValidator());

            RuleFor(x => x.TransitionEndRule)
                .SetValidator(new TransitionRuleValidator());
        }
    }
}





