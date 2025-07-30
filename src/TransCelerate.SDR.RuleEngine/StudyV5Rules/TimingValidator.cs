using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
   public class TimingValidator : AbstractValidator<TimingDto>
   {
      private readonly IHttpContextAccessor _httpContextAccessor;
      public TimingValidator(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
         RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.Id)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.Name)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.Label)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.Label)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.InstanceType)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.InstanceType)), ApplyConditionTo.AllValidators)
            .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

         RuleFor(x => x.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.Type)), ApplyConditionTo.AllValidators)
            .SetValidator(new CodeValidator(_httpContextAccessor));

         RuleFor(x => x.Value)
             .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
             .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
             .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.Value)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.ValueLabel)
             .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
             .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
             .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.ValueLabel)), ApplyConditionTo.AllValidators);


         RuleFor(x => x.RelativeFromScheduledInstanceId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.RelativeFromScheduledInstanceId)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.RelativeToScheduledInstanceId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.RelativeToScheduledInstanceId)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.WindowLabel)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
            .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.WindowLabel)), ApplyConditionTo.AllValidators);

         RuleFor(x => x.RelativeToFrom)
           .Cascade(CascadeMode.Stop)
           .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
           .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
           .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(TimingValidator), nameof(TimingDto.RelativeToFrom)), ApplyConditionTo.AllValidators)
           .SetValidator(new CodeValidator(_httpContextAccessor));

         RuleFor(x => x)
            .Must(x => HasFullyDefinedTimingWindows(x)).WithMessage(Constants.ValidationErrorMessage.DDF00006)
            .Must(x => NoWindowForAnchorTiming(x)).WithMessage(Constants.ValidationErrorMessage.DDF00025)
            .Must(x => FixedReferenceTimingIsRelativeToFromStartToStart(x)).WithMessage(Constants.ValidationErrorMessage.DDF00036)
            .Must(x => FixedReferenceTimingPointsToOnlyOneScheduledInstance(x)).WithMessage(Constants.ValidationErrorMessage.DDF00007)
            .Must(x => NonFixedReferenceTimingsPointToTwoDifferentScheduledInstances(x)).WithMessage(Constants.ValidationErrorMessage.DDF00031);
      }

      /// <summary>
      /// DDF00006 - Timing windows must be fully defined, if one of the window attributes (i.e., window label, window lower, and window upper) is defined then all must be specified.
      /// </summary>
      public static bool HasFullyDefinedTimingWindows(TimingDto timing)
      {
         if (timing == null) return true;

         bool hasWindowLabel = !string.IsNullOrWhiteSpace(timing.WindowLabel);
         bool hasWindowLower = !string.IsNullOrWhiteSpace(timing.WindowLower);
         bool hasWindowUpper = !string.IsNullOrWhiteSpace(timing.WindowUpper);

         // Return true if all are specified or none are specified, otherwise return false
         return (hasWindowLabel && hasWindowLower && hasWindowUpper) ||
               (!hasWindowLabel && !hasWindowLower && !hasWindowUpper);
      }

      /// <summary>
      /// DDF00025 - A window must not be defined for an anchor timing (i.e., type is "Fixed Reference").
      /// </summary>
      public static bool NoWindowForAnchorTiming(TimingDto timing)
      {
         if (timing == null || timing.Type == null || string.IsNullOrWhiteSpace(timing.Type.Decode))
            return true;

         bool isAnchorTiming = timing.Type.Decode.Equals(Constants.TimingType.FIXED_REFERENCE, StringComparison.OrdinalIgnoreCase);

         if (!isAnchorTiming)
            return true;

         // If it's an anchor timing, none of the window fields should be defined
         return string.IsNullOrWhiteSpace(timing.WindowLabel) &&
                string.IsNullOrWhiteSpace(timing.WindowLower) &&
                string.IsNullOrWhiteSpace(timing.WindowUpper);
      }

      /// <summary>
      /// DDF00036 - If timing type is "Fixed Reference" then the corresponding attribute relativeToFrom must be filled with "Start to Start".
      /// </summary>
      public static bool FixedReferenceTimingIsRelativeToFromStartToStart(TimingDto timing)
      {
         if (timing == null || timing.Type == null || string.IsNullOrWhiteSpace(timing.Type.Decode))
            return true;

         bool isAnchorTiming = timing.Type.Decode.Equals(Constants.TimingType.FIXED_REFERENCE, StringComparison.OrdinalIgnoreCase);

         if (!isAnchorTiming)
            return true;

         // If it's an anchor timing, the RelativeToFrom must be filled with "Start to Start"
         return timing.RelativeToFrom != null && timing.RelativeToFrom.Decode != null &&
            timing.RelativeToFrom.Decode.Equals(Constants.TimingType.START_TO_START, StringComparison.OrdinalIgnoreCase);
      }

      /// <summary>
      /// DDF00007 - If timing type is "Fixed Reference" then it must point to only one scheduled instance 
      /// (e.g. attribute relativeToScheduledInstance must be equal to relativeFromScheduledInstance or it must be missing).
      /// </summary>
      public static bool FixedReferenceTimingPointsToOnlyOneScheduledInstance(TimingDto timing)
      {
         if (timing == null || timing.Type == null || string.IsNullOrWhiteSpace(timing.Type.Decode))
            return true;

         bool isAnchorTiming = timing.Type.Decode.Equals(Constants.TimingType.FIXED_REFERENCE, StringComparison.OrdinalIgnoreCase);

         if (!isAnchorTiming)
            return true;

         // If either scheduled instance reference is null, or both are equal, return true. Otherwise return false.
         return string.IsNullOrWhiteSpace(timing.RelativeToScheduledInstanceId) ||
                string.IsNullOrWhiteSpace(timing.RelativeFromScheduledInstanceId) ||
                timing.RelativeToScheduledInstanceId == timing.RelativeFromScheduledInstanceId;
      }

      /// <summary>
      /// DDF00031 - If timing type is not "Fixed Reference" then it must point to two scheduled instances (e.g. the relativeFromScheduledInstance and relativeToScheduledInstance attributes must not be missing and must not be equal to each other).
      /// </summary>
      public static bool NonFixedReferenceTimingsPointToTwoDifferentScheduledInstances(TimingDto timing)
      {
         if (timing == null || timing.Type == null || string.IsNullOrWhiteSpace(timing.Type.Decode))
            return true;

         bool isAnchorTiming = timing.Type.Decode.Equals(Constants.TimingType.FIXED_REFERENCE, StringComparison.OrdinalIgnoreCase);

         if (isAnchorTiming)
            return true;

         // For non-fixed reference, both IDs must be present and different
         return !string.IsNullOrWhiteSpace(timing.RelativeFromScheduledInstanceId) &&
                !string.IsNullOrWhiteSpace(timing.RelativeToScheduledInstanceId) &&
                !timing.RelativeFromScheduledInstanceId.Equals(timing.RelativeToScheduledInstanceId, StringComparison.Ordinal);
      }
    }
}
