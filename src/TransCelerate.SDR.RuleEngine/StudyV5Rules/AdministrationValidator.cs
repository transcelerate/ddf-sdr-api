using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.RuleEngineV5.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This class is the validator for Administration
    /// </summary>
    public class AdministrationValidator : AbstractValidator<AdministrationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HashSet<string> _requiredProperties = new()
        {
            nameof(AdministrationDto.Id),
            nameof(AdministrationDto.InstanceType),
            nameof(AdministrationDto.Name),
            nameof(AdministrationDto.Duration)
        };

        public AdministrationValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Id)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Id), _requiredProperties);

            RuleFor(x => x.InstanceType)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.InstanceType), _requiredProperties)
                .MustMatchValidatorInstanceType(this.GetType().Name);

            RuleFor(x => x.Name)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Name), _requiredProperties);

            RuleFor(x => x.Description)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Description), _requiredProperties);

            RuleFor(x => x.Label)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Label), _requiredProperties);

            RuleFor(x => x.Frequency)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Frequency), _requiredProperties)
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.Route)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Route), _requiredProperties)
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.Duration)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Duration), _requiredProperties)
                .SetValidator(new DurationValidator(_httpContextAccessor));

            RuleFor(x => x.Dose)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Dose), _requiredProperties)
                .SetValidator(new QuantityValidator(_httpContextAccessor));

            RuleFor(x => x.AdministrableProduct)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.AdministrableProduct), _requiredProperties)
                .SetValidator(new AdministrableProductValidator(_httpContextAccessor));

            RuleFor(x => x.Notes)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.Notes), _requiredProperties);

            RuleFor(x => x.MedicalDevice)
                .NotNullOrEmptyIfRequired(nameof(AdministrationDto.MedicalDevice), _requiredProperties)
                .SetValidator(new MedicalDeviceValidator(_httpContextAccessor));

            RuleForEach(x => x.Notes)
                .SetValidator(new CommentAnnotationValidator(_httpContextAccessor));
        }
    }
}
