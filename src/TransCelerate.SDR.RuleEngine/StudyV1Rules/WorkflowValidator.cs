using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for WorkFlows
    /// </summary>
    public class WorkflowValidator : AbstractValidator<WorkflowDto>
    {
        public WorkflowValidator()
        {
            RuleFor(x => x.WorkflowDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            
            RuleFor(x => x.WorkflowItems)
               .ForEach(y => y.SetValidator(new WorkflowItemValidator()));

        }
    }
}





