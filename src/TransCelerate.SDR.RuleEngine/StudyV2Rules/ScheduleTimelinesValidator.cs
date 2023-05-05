﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    public class ScheduleTimelinesValidator : AbstractValidator<ScheduleTimelineDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduleTimelinesValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduleTimelineId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduleTimelineId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.ScheduleTimelineName)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineEntryId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.ScheduleTimelineEntryId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.ScheduleTimelineDescription)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.EntryCondition)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.EntryCondition)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineExits)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.ScheduleTimelineExits)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError)
               .ForEach(x => x.SetValidator(new ScheduleTimelineExitValidator(_httpContextAccessor)));


            RuleFor(x => x.ScheduleTimelineInstances)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelinesValidator), nameof(ScheduleTimelineDto.ScheduleTimelineInstances)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError)
                .ForEach(x => x.SetValidator(new ScheduledInstanceValidator(_httpContextAccessor)));

            RuleForEach(x => x.ScheduleTimelineInstances).SetInheritanceValidator(v =>
            {
                v.Add(new ScheduledActivityInstanceValidator(_httpContextAccessor));
                v.Add(new ScheduledDecisionInstanceValidator(_httpContextAccessor));
            });
        }
    }
}
