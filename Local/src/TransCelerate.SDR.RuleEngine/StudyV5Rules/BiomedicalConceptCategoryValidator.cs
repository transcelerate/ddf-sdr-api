using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for Activity
    /// </summary>
    public class BiomedicalConceptCategoryValidator : AbstractValidator<BiomedicalConceptCategoryDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BiomedicalConceptCategoryValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.InstanceType)), ApplyConditionTo.AllValidators)
              .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Description)), ApplyConditionTo.AllValidators);            

            RuleFor(x => x.ChildIds)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.ChildIds)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.MemberIds)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.MemberIds)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Code)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Code)), ApplyConditionTo.AllValidators)
               .SetValidator(new AliasCodeValidator(_httpContextAccessor));
            RuleFor(x => x.Notes)
			   .Cascade(CascadeMode.Stop)
			   .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
			   .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
			   .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptCategoryValidator), nameof(BiomedicalConceptCategoryDto.Notes)), ApplyConditionTo.AllValidators);
		}
    }
}





