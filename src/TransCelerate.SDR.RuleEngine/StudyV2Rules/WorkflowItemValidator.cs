using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for WorkFlowItems
    /// </summary>
    public class WorkflowItemValidator : AbstractValidator<WorkflowItemDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WorkflowItemValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.WorkflowItemDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.WorkflowItemDescription)), ApplyConditionTo.AllValidators);           

            RuleFor(x => x.WorkflowItemActivityId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.WorkflowItemActivityId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.WorkflowItemEncounterId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.WorkflowItemEncounterId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.NextWorkflowItemId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.NextWorkflowItemId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PreviousWorkflowItemId)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowItemValidator), nameof(WorkflowItemDto.PreviousWorkflowItemId)), ApplyConditionTo.AllValidators);

        }
    }
}





