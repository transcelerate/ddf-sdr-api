using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for Code
    /// </summary>
    public class CommentAnnotationValidator : AbstractValidator<CommentAnnotationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentAnnotationValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(CommentAnnotationValidator), nameof(CommentAnnotationDto.Id)), ApplyConditionTo.AllValidators);
			RuleFor(x => x.Codes)
				.Cascade(CascadeMode.Stop)
				.NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
				.NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
				.When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(CommentAnnotationValidator), nameof(CommentAnnotationDto.Codes)), ApplyConditionTo.AllValidators);

			RuleFor(x => x.InstanceType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(CommentAnnotationValidator), nameof(CommentAnnotationDto.InstanceType)), ApplyConditionTo.AllValidators)
                .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Text)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(CommentAnnotationValidator), nameof(CommentAnnotationDto.Text)), ApplyConditionTo.AllValidators);            
        }
    }
}
