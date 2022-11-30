using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for WorkFlowItems
    /// </summary>
    public class WorkflowItemValidator : AbstractValidator<WorkflowItemDto>
    {
        public WorkflowItemValidator()
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.WorkflowItemDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

        }
    }
}





