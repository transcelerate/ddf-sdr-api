using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for Study
    /// </summary>
    public class StudyDefinitionsValidator : AbstractValidator<StudyDefinitionsDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyDefinitionsValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Study)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.RootElementMissing)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDefinitionsValidator), nameof(StudyDefinitionsDto.Study)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyValidator(_httpContextAccessor));

            RuleFor(x => x.UsdmVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDefinitionsValidator), nameof(StudyDefinitionsDto.UsdmVersion)), ApplyConditionTo.AllValidators)
                .Must(x => x == _httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion]).WithMessage(Constants.ValidationErrorMessage.UsdmVersionMismatch);
                
            RuleFor(x => x.SystemName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDefinitionsValidator), nameof(StudyDefinitionsDto.SystemName)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.SystemVersion)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDefinitionsValidator), nameof(StudyDefinitionsDto.SystemVersion)), ApplyConditionTo.AllValidators);


        }
    }
}
