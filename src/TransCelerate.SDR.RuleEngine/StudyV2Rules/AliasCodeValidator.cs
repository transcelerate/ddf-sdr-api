using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for AliasCode
    /// </summary>
    public class AliasCodeValidator:AbstractValidator<AliasCodeDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AliasCodeValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.AliasCodeId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.AliasCodeId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(AliasCodeValidator), nameof(AliasCodeDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x=>x.StandardCode)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(AliasCodeValidator), nameof(AliasCodeDto.StandardCode)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StandardCodeAliases)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(AliasCodeValidator), nameof(AliasCodeDto.StandardCodeAliases)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}
