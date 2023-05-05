using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyObjectives
    /// </summary>
    public class StudyObjectiveValidator : AbstractValidator<ObjectiveDto>
    {
        public StudyObjectiveValidator()
        {
            RuleFor(x => x.ObjectiveDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.ObjectiveEndpoints)                
                .ForEach(y => y.SetValidator(new EndpointValidator()));

            RuleFor(x => x.ObjectiveLevel)
                .ForEach(y => y.SetValidator(new CodeValidator()));
        }
    }
}





