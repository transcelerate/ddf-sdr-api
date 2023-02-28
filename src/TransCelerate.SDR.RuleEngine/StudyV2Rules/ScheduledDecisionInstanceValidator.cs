using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.RuleEngineV2
{
    public class ScheduledDecisionInstanceValidator : AbstractValidator<ScheduledDecisionInstanceDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduledDecisionInstanceValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduledInstanceId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduledInstanceId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineExitId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduleTimelineExitId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduledInstanceEncounterId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduledInstanceEncounterId)), ApplyConditionTo.AllValidators);
            
            RuleFor(x => x.ScheduledInstanceTimelineId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduledInstanceTimelineId)), ApplyConditionTo.AllValidators);            

            RuleFor(x => x.ScheduleSeqenceNumber)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduleSeqenceNumber)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduledInstanceTimings)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduledInstanceTimings)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.ScheduledInstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ScheduledInstanceType)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ConditionAssignments)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledDecisionInstanceValidator), nameof(ScheduledDecisionInstanceDto.ConditionAssignments)), ApplyConditionTo.AllValidators);
        }
    }
}
