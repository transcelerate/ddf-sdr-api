using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyObjectives
    /// </summary>
    public class StudyObjectiveValidator : AbstractValidator<ObjectiveDto>
    {
        public StudyObjectiveValidator()
        {
            RuleFor(x => x.Uuid)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.ObjectiveDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);    

            RuleFor(x => x.ObjectiveLevel)
               .Must(x => UniquenessArrayValidator.ValidateArray(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
            
            RuleFor(x => x.ObjectiveEndpoints)
               .Must(x => UniquenessArrayValidator.ValidateArray(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}





