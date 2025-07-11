using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for Strength
    /// </summary>
    public class StrengthValidator : AbstractValidator<StrengthDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StrengthValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.InstanceType)), ApplyConditionTo.AllValidators)
              .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.NumeratorQuantity)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.NumeratorQuantity)), ApplyConditionTo.AllValidators)
                .SetValidator(new QuantityValidator(_httpContextAccessor));

            RuleFor(x => x.Denominator)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.Denominator)), ApplyConditionTo.AllValidators)
                .SetValidator(new QuantityValidator(_httpContextAccessor));
                
            RuleFor(x => x.NumeratorRange)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StrengthValidator), nameof(StrengthDto.NumeratorRange)), ApplyConditionTo.AllValidators)     
                .SetValidator(new RangeValidator(_httpContextAccessor));
        }
    }
}
