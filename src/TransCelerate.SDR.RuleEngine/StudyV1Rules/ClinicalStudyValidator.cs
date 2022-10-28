using FluentValidation;
using System;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for ClinicalStudy
    /// </summary>
    public class ClinicalStudyValidator : AbstractValidator<ClinicalStudyDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClinicalStudyValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Uuid)
                .Must(x => UUIDConformanceValidationHelper.CheckForUUIDConformance(x, httpContextAccessor?.HttpContext?.Request?.Method))
                .WithMessage(x => UUIDConformanceValidationHelper.GetMessageForUUIDConformance(x.Uuid));

            RuleFor(x => x.StudyTitle)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyIdentifiers)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x=> UniquenessArrayValidator.ValidateArray(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyPhase)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyProtocolVersions)
                .Must(x => UniquenessArrayValidator.ValidateArray(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x=>x.StudyDesigns)
                .Must(x => UniquenessArrayValidator.ValidateArray(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

        }
    }
}
