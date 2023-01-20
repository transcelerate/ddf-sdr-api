﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for WorkFlows
    /// </summary>
    public class WorkflowValidator : AbstractValidator<WorkflowDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WorkflowValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowValidator), nameof(WorkflowDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.WorkflowDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowValidator), nameof(WorkflowDto.WorkflowDescription)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.WorkflowItems)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(WorkflowValidator), nameof(WorkflowDto.WorkflowItems)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}





