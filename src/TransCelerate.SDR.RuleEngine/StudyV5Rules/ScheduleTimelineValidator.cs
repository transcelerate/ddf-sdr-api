using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    public class ScheduleTimelineValidator : AbstractValidator<ScheduleTimelineDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduleTimelineValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.InstanceType)), ApplyConditionTo.AllValidators)
               .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.EntryId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.EntryId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.EntryCondition)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.EntryCondition)), ApplyConditionTo.AllValidators);
            
            RuleFor(x => x.MainTimeline)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.MainTimeline)), ApplyConditionTo.AllValidators)
               .Must(ValidateDatatype.ValidateBoolean).WithMessage(Constants.ValidationErrorMessage.BooleanValidationFailed);

            RuleFor(x => x.PlannedDuration)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.PlannedDuration)), ApplyConditionTo.AllValidators)
               .SetValidator(new DurationValidator(_httpContextAccessor));

            RuleFor(x => x.Exits)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Exits)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Exits)
                .SetValidator(new ScheduleTimelineExitValidator(_httpContextAccessor));

            RuleFor(x => x.Timings)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Timings)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Timings)
                .SetValidator(new TimingValidator(_httpContextAccessor));

            RuleFor(x => x.Instances)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ScheduleTimelineValidator), nameof(ScheduleTimelineDto.Instances)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Instances)
                .SetValidator(new ScheduledInstanceValidator(_httpContextAccessor));

            RuleForEach(x => x.Instances).SetInheritanceValidator(v =>
            {
                v.Add(new ScheduledActivityInstanceValidator(_httpContextAccessor));
                v.Add(new ScheduledDecisionInstanceValidator(_httpContextAccessor));
            });
        }
    }
}
