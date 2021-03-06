using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    public class StudyObjectiveValidator : AbstractValidator<ObjectiveDto>
    {
        public StudyObjectiveValidator()
        {
            RuleFor(x => x.ObjectiveDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);    
        }
    }
}





