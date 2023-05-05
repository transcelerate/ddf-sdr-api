using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for WorkFlowItems
    /// </summary>
    public class WorkflowItemValidator : AbstractValidator<WorkflowItemDto>
    {
        public WorkflowItemValidator()
        {
            RuleFor(x => x.WorkflowItemDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            
            RuleFor(x => x.WorkflowItemActivity)
                .SetValidator(new ActivityValidator());
            
            RuleFor(x => x.WorkflowItemEncounter)
                .SetValidator(new EncounterValidator());
        }
    }
}





