using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Study
    /// </summary>
    public class StudyDefinitionsValidator : AbstractValidator<StudyDefinitionsDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyDefinitionsValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Study)
                .Cascade(CascadeMode.Stop)
                .NotNull().OverridePropertyName(IdFieldPropertyName.ParentElement.ClinicalStudy).WithName(IdFieldPropertyName.ParentElement.ClinicalStudy).WithMessage(Constants.ValidationErrorMessage.RootElementMissing)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDefinitionsValidator), nameof(StudyDefinitionsDto.Study)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyValidator(_httpContextAccessor));
        }
    }
}
