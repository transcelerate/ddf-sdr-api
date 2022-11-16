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
            RuleFor(x => x.Uuid)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.WorkflowItemDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.NextWorkflowItemId)
                .Cascade(CascadeMode.Stop).
                NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError);

            RuleFor(x => x.PreviousWorkflowItemId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError);



        }
    }
}





