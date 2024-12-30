using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV3
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
              .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV3.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV3.StudyCellId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyElementIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyElementIds)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyArmId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyArmId)), ApplyConditionTo.AllValidators);                

            RuleFor(x => x.StudyEpochId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellsValidator), nameof(StudyCellDto.StudyEpochId)), ApplyConditionTo.AllValidators);                

        }
    }
}





