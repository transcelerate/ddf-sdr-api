using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for StudyAmendment
    /// </summary>
    public class StudyAmendmentValidator : AbstractValidator<StudyAmendmentDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyAmendmentValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.InstanceType)), ApplyConditionTo.AllValidators)
                .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Number)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Number)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Summary)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Summary)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.DateValues)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.DateValues)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.DateValues)
                .SetValidator(new GovernanceDateValidator(httpContextAccessor));

            RuleFor(x => x.GeographicScopes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.GeographicScopes)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.GeographicScopes)
                .SetValidator(new GeographicScopeValidator(httpContextAccessor));

            RuleFor(x => x.Changes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Changes)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Changes)
                .SetValidator(new StudyChangeValidator(httpContextAccessor));

            RuleFor(x => x.Impacts)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Impacts)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Impacts)
                .SetValidator(new StudyAmendmentImpactValidator(httpContextAccessor));

            RuleFor(x => x.Enrollments)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Enrollments)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Enrollments)
                .SetValidator(new SubjectEnrollmentValidator(httpContextAccessor));

            RuleFor(x => x.PreviousId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.PreviousId)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.PrimaryReason)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.PrimaryReason)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyAmendmentReasonValidator(httpContextAccessor));

            RuleFor(x => x.SecondaryReasons)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.SecondaryReasons)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.SecondaryReasons)
                .SetValidator(new StudyAmendmentReasonValidator(httpContextAccessor));
                
            RuleFor(x => x.Notes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyAmendmentValidator), nameof(StudyAmendmentDto.Notes)), ApplyConditionTo.AllValidators);
            
            RuleForEach(x => x.Notes)
                .SetValidator(new CommentAnnotationValidator(httpContextAccessor));
        }
    }
}
