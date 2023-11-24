using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for StudyDesignPopulation
    /// </summary>
    public class StudyDesignPopulationValidator : AbstractValidator<StudyDesignPopulationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyDesignPopulationValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PlannedNumberOfParticipants)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.PlannedNumberOfParticipants)), ApplyConditionTo.AllValidators)
               .Must(ValidateDatatype.ValidateInt).WithMessage(Constants.ValidationErrorMessage.IntegerValidationFailed)
               .Must(x => Convert.ToInt32(x) >= Constants.DefaultValues.IntegerMinimumValue).WithMessage(Constants.ValidationErrorMessage.IntegerMinimumValueError);

            RuleFor(x => x.Label)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PlannedMaximumAgeOfParticipants)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.PlannedMaximumAgeOfParticipants)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PlannedMinimumAgeOfParticipants)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.PlannedMinimumAgeOfParticipants)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PlannedSexOfParticipants)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignPopulationValidator), nameof(StudyDesignPopulationDto.PlannedSexOfParticipants)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.PlannedSexOfParticipants)
                .SetValidator(new CodeValidator(_httpContextAccessor));
        }
    }
}





