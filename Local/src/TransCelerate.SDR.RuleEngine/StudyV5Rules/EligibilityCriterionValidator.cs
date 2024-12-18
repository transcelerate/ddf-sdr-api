using FluentValidation;
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
	public class EligibilityCriterionValidator : AbstractValidator<EligibilityCriterionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EligibilityCriterionValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;            

			RuleFor(x => x.Category)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Category)), ApplyConditionTo.AllValidators)
			   .SetValidator(new CodeValidator(_httpContextAccessor));

			RuleFor(x => x.Identifier)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Identifier)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.PreviousId)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.PreviousId)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.NextId)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.NextId)), ApplyConditionTo.AllValidators);

			//RuleFor(x => x.Notes)
			//   .Cascade(CascadeMode.Stop)
			//   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			//   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			//   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Notes)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.ContextId)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.ContextId)), ApplyConditionTo.AllValidators);
		}
    }
}





