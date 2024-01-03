﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for StudyProtocolVersions
    /// </summary>
    public class StudyProtocolDocumentVersionValidator : AbstractValidator<StudyProtocolDocumentVersionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyProtocolDocumentVersionValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BriefTitle)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.BriefTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.OfficialTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.OfficialTitle)), ApplyConditionTo.AllValidators);            

            RuleFor(x => x.ProtocolStatus)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.ProtocolStatus)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.ProtocolVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.ProtocolVersion)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PublicTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.PublicTitle)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ScientificTitle)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.ScientificTitle)), ApplyConditionTo.AllValidators);            

            RuleFor(x => x.ChildrenIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.ChildrenIds)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.DateValues)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.DateValues)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.DateValues)
                .SetValidator(new GovernanceDateValidator(httpContextAccessor));

            RuleFor(x => x.Contents)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyProtocolDocumentVersionValidator), nameof(StudyProtocolDocumentVersionDto.Contents)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Contents)
               .SetValidator(new NarrativeContentValidator(httpContextAccessor));
        }
    }
}
