using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for Encounter
    /// </summary>
    public class EncounterValidator : AbstractValidator<EncounterDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EncounterValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ContactModes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.ContactModes)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.ContactModes)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.EnvironmentalSetting)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.EnvironmentalSetting)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.Type)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.Type)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.NextId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.NextId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PreviousId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.PreviousId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.TransitionStartRule)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.TransitionStartRule)), ApplyConditionTo.AllValidators)
                .SetValidator(new TransitionRuleValidator(_httpContextAccessor));

            RuleFor(x => x.TransitionEndRule)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.TransitionEndRule)), ApplyConditionTo.AllValidators)
                .SetValidator(new TransitionRuleValidator(_httpContextAccessor));

            RuleFor(x => x.ScheduledAtTimingId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(EncounterValidator), nameof(EncounterDto.ScheduledAtTimingId)), ApplyConditionTo.AllValidators);
        }
    }
}





