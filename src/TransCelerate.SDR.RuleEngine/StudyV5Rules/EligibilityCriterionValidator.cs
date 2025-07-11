using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
	/// <summary>
	/// This Class is the validator for EligibilityCriterion
	/// </summary>
	public class EligibilityCriterionValidator : AbstractValidator<EligibilityCriterionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
		
        public EligibilityCriterionValidator(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;

			RuleFor(x => x.Id)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Id)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.InstanceType)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.InstanceType)), ApplyConditionTo.AllValidators)
			   .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

			
			RuleFor(x => x.Name)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Name)), ApplyConditionTo.AllValidators);

			
			RuleFor(x => x.Label)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Label)), ApplyConditionTo.AllValidators);

			
			RuleFor(x => x.Description)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Description)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.Identifier)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Identifier)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.Category)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Category)), ApplyConditionTo.AllValidators)
			   .SetValidator(new CodeValidator(_httpContextAccessor));

			RuleFor(x => x.Notes)
			    .Cascade(CascadeMode.Stop)
			    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			    .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.Notes)), ApplyConditionTo.AllValidators);

			RuleForEach(x => x.Notes)
   			    .SetValidator(new CommentAnnotationValidator(_httpContextAccessor));
			
			
			RuleFor(x => x.CriterionItem)
			    .Cascade(CascadeMode.Stop)
			    .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			    .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			    .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EligibilityCriterionValidator), nameof(EligibilityCriterionDto.CriterionItem)), ApplyConditionTo.AllValidators)
			    .SetValidator(new EligibilityCriterionItemValidator(_httpContextAccessor));

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
		}
    }
}





