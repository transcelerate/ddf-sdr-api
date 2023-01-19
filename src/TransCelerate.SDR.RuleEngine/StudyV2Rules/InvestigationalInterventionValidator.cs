using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for InvestigationalIntervention
    /// </summary>
    public class InvestigationalInterventionValidator : AbstractValidator<InvestigationalInterventionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvestigationalInterventionValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.InvestigationalInterventionId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.InvestigationalInterventionId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InterventionDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.InterventionDescription)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Codes)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.Codes)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}





