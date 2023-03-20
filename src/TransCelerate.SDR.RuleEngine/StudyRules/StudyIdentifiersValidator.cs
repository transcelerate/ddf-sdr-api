using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyIdentifiersValidator : AbstractValidator<StudyIdentifierDTO>
    {
        /// <summary>
        /// Validator for studyIdentifiers
        /// </summary>
        public StudyIdentifiersValidator()
        {
            RuleFor(x => x.IdType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.OrgCode)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}
