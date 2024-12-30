﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
	/// <summary>
	/// This Class is the validator for NarrativeContentItem
	/// </summary>
	public class NarrativeContentItemValidator : AbstractValidator<NarrativeContentItemDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NarrativeContentItemValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
			RuleFor(x => x.Id)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(NarrativeContentItemValidator), nameof(NarrativeContentItemDto.Id)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.InstanceType)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(NarrativeContentItemValidator), nameof(NarrativeContentItemDto.InstanceType)), ApplyConditionTo.AllValidators)
			   .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

			RuleFor(x => x.Name)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(NarrativeContentItemValidator), nameof(NarrativeContentItemDto.Name)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.Text)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(NarrativeContentItemValidator), nameof(NarrativeContentItemDto.Text)), ApplyConditionTo.AllValidators);
		}
    }
}





