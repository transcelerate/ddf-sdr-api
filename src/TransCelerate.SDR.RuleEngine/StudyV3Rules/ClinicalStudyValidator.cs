﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV3
{
    /// <summary>
    /// This Class is the validator for ClinicalStudy
    /// </summary>
    public class ClinicalStudyValidator : AbstractValidator<ClinicalStudyDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClinicalStudyValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.StudyTitle)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyType)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.StudyIdentifiers)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyIdentifiers)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV3(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyIdentifiers)
                .SetValidator(new StudyIdentifiersValidator(_httpContextAccessor));

            RuleFor(x => x.StudyPhase)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyPhase)), ApplyConditionTo.AllValidators)
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.StudyVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyVersion)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyAcronym)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyAcronym)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyRationale)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyRationale)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyProtocolVersions).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyProtocolVersions)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV3(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyProtocolVersions)
                .SetValidator(new StudyProtocolVersionsValidator(_httpContextAccessor));

            RuleFor(x => x.StudyDesigns).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.StudyDesigns)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV3(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyDesigns)
                .SetValidator(new StudyDesignValidator(_httpContextAccessor));

            RuleFor(x => x.BusinessTherapeuticAreas).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ClinicalStudyValidator), nameof(ClinicalStudyDto.BusinessTherapeuticAreas)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV3(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.BusinessTherapeuticAreas)
                .SetValidator(new CodeValidator(_httpContextAccessor));
        }
    }
}
