using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Activity
    /// </summary>
    public class BiomedicalConceptSurrogateValidator : AbstractValidator<BiomedicalConceptSurrogateDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BiomedicalConceptSurrogateValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.BcSurrogateId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.BcSurrogateId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptSurrogateValidator), nameof(BiomedicalConceptSurrogateDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BcSurrogateName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptSurrogateValidator), nameof(BiomedicalConceptSurrogateDto.BcSurrogateName)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BcSurrogateDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptSurrogateValidator), nameof(BiomedicalConceptSurrogateDto.BcSurrogateDescription)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BcSurrogateReference)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(BiomedicalConceptSurrogateValidator), nameof(BiomedicalConceptSurrogateDto.BcSurrogateReference)), ApplyConditionTo.AllValidators);
        }
    }
}





