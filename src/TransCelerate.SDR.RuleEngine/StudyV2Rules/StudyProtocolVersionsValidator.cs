﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for StudyProtocolVersions
    /// </summary>
    public class StudyProtocolVersionsValidator : AbstractValidator<StudyProtocolVersionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyProtocolVersionsValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyProtocolVersionId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyProtocolVersionId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BriefTitle)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.BriefTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.OfficialTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.OfficialTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ProtocolEffectiveDate)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.ProtocolEffectiveDate)), ApplyConditionTo.AllValidators);
                //.Must(x => DateValidationHelper.IsValid(x)).WithMessage(Constants.ValidationErrorMessage.ValidDateError);

            RuleFor(x => x.ProtocolStatus)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.ProtocolStatus)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.ProtocolVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.ProtocolVersion)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PublicTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.PublicTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScientificTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.ScientificTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ProtocolAmendment)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolVersionsValidator), nameof(StudyProtocolVersionDto.ProtocolAmendment)), ApplyConditionTo.AllValidators);
        }
    }
}
