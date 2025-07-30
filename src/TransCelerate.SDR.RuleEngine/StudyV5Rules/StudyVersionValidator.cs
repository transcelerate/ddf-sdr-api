using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine.Common;
using TransCelerate.SDR.RuleEngine.Utilities.Common;
using TransCelerate.SDR.RuleEngineV5;

namespace TransCelerate.SDR.RuleEngine.StudyV5Rules
{
    /// <summary>
    /// This class is the validator for Study
    /// </summary>
    public class StudyVersionValidator : AbstractValidator<StudyVersionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HashSet<string> _requiredFields = new()
        {
            nameof(StudyVersionDto.Id),
            nameof(StudyVersionDto.InstanceType),
            nameof(StudyVersionDto.VersionIdentifier),
            nameof(StudyVersionDto.Rationale),
            nameof(StudyVersionDto.StudyIdentifiers),
            nameof(StudyVersionDto.Titles),
        };

        public StudyVersionValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.InstanceType)), ApplyConditionTo.AllValidators)
                .Must(x => GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.VersionIdentifier)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.VersionIdentifier)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BusinessTherapeuticAreas).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.BusinessTherapeuticAreas)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Rationale)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Rationale)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Notes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Notes)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Abbreviations)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Abbreviations)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Abbreviations)
                .Cascade(CascadeMode.Stop)
                .Must(x => AbbreviationValidator.ValidateAbbreviatedText(x))
                .WithMessage(RuleConstants.RuleValidationErrorMessages.DDF00170)
                .WithErrorCode(nameof(RuleConstants.RuleValidationErrorMessages.DDF00170));

            RuleFor(x => x.DateValues)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.DateValues)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ReferenceIdentifiers)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.ReferenceIdentifiers)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Amendments).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Amendments)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.DocumentVersionIds)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(StudyVersionDto.DocumentVersionIds)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.StudyDesigns).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.StudyDesigns)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyIdentifiers)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => _requiredFields.Contains(nameof(StudyVersionDto.StudyIdentifiers)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Titles)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(StudyVersionDto.Titles)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError)
               .Must(x => x.Where(y => y.Type != null).Select(y => y.Type.Decode == Constants.StudyTitle.OfficialStudyTitle).Count() > 0).WithMessage(Constants.ValidationErrorMessage.OfficialTitleError);

            RuleFor(x => x.Abbreviations)
                .Must(x => AbbreviationValidator.ValidateExpandedText(x))
                .WithMessage(RuleConstants.RuleValidationWarningMessages.DDF00171)
                .WithErrorCode(nameof(RuleConstants.RuleValidationWarningMessages.DDF00171))
                .WithSeverity(Severity.Warning);

            RuleForEach(x => x.BusinessTherapeuticAreas)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleForEach(x => x.Notes)
                .SetValidator(new CommentAnnotationValidator(_httpContextAccessor));

            RuleForEach(x => x.Abbreviations)
                .SetValidator(new AbbreviationValidator());

            RuleForEach(x => x.DateValues)
                .SetValidator(new GovernanceDateValidator(httpContextAccessor));

            RuleForEach(x => x.ReferenceIdentifiers)
                .SetValidator(new ReferenceIdentifierValidator(_httpContextAccessor));

            RuleForEach(x => x.Amendments)
                .SetValidator(new StudyAmendmentValidator(_httpContextAccessor));

            RuleForEach(x => x.StudyDesigns)
                .SetValidator(new StudyDesignValidator(_httpContextAccessor));

            RuleForEach(x => x.StudyIdentifiers)
                .SetValidator(new StudyIdentifierValidator(_httpContextAccessor));

            RuleForEach(x => x.Titles)
                .SetValidator(new StudyTitleValidator(_httpContextAccessor));
        }
    }
}
