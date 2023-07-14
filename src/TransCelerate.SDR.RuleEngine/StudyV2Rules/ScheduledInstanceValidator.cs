using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    public class ScheduledInstanceValidator : AbstractValidator<ScheduledInstanceDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduledInstanceValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduledInstanceId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.ScheduledInstanceId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleTimelineExitId)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduleTimelineExitId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduledInstanceEncounterId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduledInstanceEncounterId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduledInstanceTimelineId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduledInstanceTimelineId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScheduleSequenceNumber)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduleSequenceNumber)), ApplyConditionTo.AllValidators)
               .Must(ValidateDatatype.ValidateInt).WithMessage(Constants.ValidationErrorMessage.IntegerValidationFailed);

            RuleFor(x => x.ScheduledInstanceTimings)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduledInstanceTimings)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.ScheduledInstanceTimings)
                .SetValidator(new TimingValidator(_httpContextAccessor));

            RuleFor(x => x.ScheduledInstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduledInstanceType)), ApplyConditionTo.AllValidators)
              .Must(x => Enum.GetNames(typeof(ScheduledInstanceType)).Contains(x)).WithMessage(Constants.ValidationErrorMessage.ScheduledInstanceTypesError);
        }
    }
}
