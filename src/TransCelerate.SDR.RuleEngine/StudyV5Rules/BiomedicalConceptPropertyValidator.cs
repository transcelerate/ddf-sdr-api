using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine.Common;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for Activity
    /// </summary>
    public class BiomedicalConceptPropertyValidator : AbstractValidator<BiomedicalConceptPropertyDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BiomedicalConceptPropertyValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.InstanceType)), ApplyConditionTo.AllValidators)
               .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.IsRequired)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.IsRequired)), ApplyConditionTo.AllValidators)
               .Must(ValidateDatatype.ValidateBoolean).WithMessage(Constants.ValidationErrorMessage.BooleanValidationFailed);

            RuleFor(x => x.IsEnabled)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.IsEnabled)), ApplyConditionTo.AllValidators)
                .Must(ValidateDatatype.ValidateBoolean).WithMessage(Constants.ValidationErrorMessage.BooleanValidationFailed);

            RuleFor(x => x.Datatype)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.Datatype)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ResponseCodes)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.ResponseCodes)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.ResponseCodes)
                .SetValidator(new ResponseCodeValidator(_httpContextAccessor));

            RuleFor(x => x.Code)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptPropertyValidator), nameof(BiomedicalConceptPropertyDto.Code)), ApplyConditionTo.AllValidators)
               .SetValidator(new AliasCodeValidator(_httpContextAccessor));
        }
    }
}





