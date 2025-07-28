using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.RuleEngineV5.Common;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for Administration
    /// </summary>
    public class AdministrationValidator : AbstractValidator<AdministrationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministrationValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var usdmVersion = _httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion];
            var validatorName = nameof(AdministrationValidator);

            RuleFor(x => x.Id)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Id));

            RuleFor(x => x.InstanceType)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.InstanceType))
                .MustMatchValidatorInstanceType(validatorName);

            RuleFor(x => x.Name)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Name));

            RuleFor(x => x.Description)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Description));

            RuleFor(x => x.Label)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Label));

            RuleFor(x => x.Frequency)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Frequency))
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.Route)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Route))
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.Duration)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Duration))
                .SetValidator(new DurationValidator(_httpContextAccessor));

            RuleFor(x => x.Dose)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Dose))
                .SetValidator(new QuantityValidator(_httpContextAccessor));

            RuleFor(x => x.AdministrableProduct)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.AdministrableProduct))
                .SetValidator(new AdministrableProductValidator(_httpContextAccessor));

            RuleFor(x => x.Notes)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.Notes));

            RuleForEach(x => x.Notes)
                .SetValidator(new CommentAnnotationValidator(_httpContextAccessor));

            RuleFor(x => x.MedicalDevice)
                .NotNullOrEmptyIfRequired(usdmVersion, validatorName, nameof(AdministrationDto.MedicalDevice))
                .SetValidator(new MedicalDeviceValidator(_httpContextAccessor));
        }
    }
}
