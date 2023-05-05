using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyObjectivesValidator : AbstractValidator<StudyObjectiveDTO>
    {
        /// <summary>
        /// Validator for studyObjectives
        /// </summary>
        public StudyObjectivesValidator()
        {
            RuleFor(x => x.Level)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.Endpoints)
                .ForEach(y => y.SetValidator(new EndpointsValidator()));
        }
    }
}
