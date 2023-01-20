﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for TransitionRule
    /// </summary>
    public class TransitionRuleValidator : AbstractValidator<TransitionRuleDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransitionRuleValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.TransitionRuleId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.TransitionRuleId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(TransitionRuleValidator), nameof(TransitionRuleDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.TransitionRuleDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(TransitionRuleValidator), nameof(TransitionRuleDto.TransitionRuleDescription)), ApplyConditionTo.AllValidators);
        }
    }
}





