using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV3
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
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV3.InvestigationalInterventionId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV3.InvestigationalInterventionId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InterventionDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.InterventionDescription)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Codes)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(InvestigationalInterventionValidator), nameof(InvestigationalInterventionDto.Codes)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV3(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Codes)
                .SetValidator(new CodeValidator(_httpContextAccessor));
        }
    }
}





