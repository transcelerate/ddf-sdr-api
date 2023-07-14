using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for StudyCells
    /// </summary>
    public class StudyCellsValidator : AbstractValidator<StudyCellDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyCellsValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
              .Cascade(CascadeMode.Stop)
              .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyElements)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyElements)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyElements)
                .SetValidator(new StudyElementsValidator(_httpContextAccessor));

            RuleFor(x => x.StudyArm)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyArm)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyArmValidator(_httpContextAccessor));

            RuleFor(x => x.StudyEpoch)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyEpoch)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyEpochValidator(_httpContextAccessor));

        }
    }
}





