using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.RuleEngine.Common;
using TransCelerate.SDR.RuleEngine.Utilities.Common;
using TransCelerate.SDR.RuleEngineV5;
using TransCelerate.SDR.RuleEngineV5.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine.StudyV5Rules
{
    /// <summary>
    /// This class is the validator for StudyVersion
    /// </summary>
    public class StudyVersionValidator : AbstractValidator<StudyVersionDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HashSet<string> _requiredProperties = new()
        {
            nameof(StudyVersionDto.Id),
            nameof(StudyVersionDto.InstanceType),
            nameof(StudyVersionDto.VersionIdentifier),
            nameof(StudyVersionDto.Rationale),
            nameof(StudyVersionDto.StudyIdentifiers),
            nameof(StudyVersionDto.Titles)
        };

        public StudyVersionValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Id)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Id), _requiredProperties);

            RuleFor(x => x.InstanceType)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.InstanceType), _requiredProperties)
                .MustMatchValidatorInstanceType(this.GetType().Name);

            RuleFor(x => x.VersionIdentifier)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.VersionIdentifier), _requiredProperties);

            RuleFor(x => x.BusinessTherapeuticAreas)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.BusinessTherapeuticAreas), _requiredProperties)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Rationale)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Rationale), _requiredProperties);

            RuleFor(x => x.Notes)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Notes), _requiredProperties);

            RuleFor(x => x.Abbreviations)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Abbreviations), _requiredProperties);

            RuleFor(x => x.Abbreviations)
                .Must(x => AbbreviationValidator.ValidateAbbreviatedText(x))
                .WithMessage(RuleConstants.RuleValidationErrorMessages.DDF00170)
                .WithErrorCode(nameof(RuleConstants.RuleValidationErrorMessages.DDF00170));

            RuleFor(x => x.DateValues)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.DateValues), _requiredProperties);

            RuleFor(x => x.ReferenceIdentifiers)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.ReferenceIdentifiers), _requiredProperties);

            RuleFor(x => x.Amendments)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Amendments), _requiredProperties)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.DocumentVersionIds)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.DocumentVersionIds), _requiredProperties);

            RuleFor(x => x.StudyDesigns)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.StudyDesigns), _requiredProperties)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyIdentifiers)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.StudyIdentifiers), _requiredProperties)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyInterventions)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.StudyInterventions), _requiredProperties)
                .Must(x => UniquenessArrayValidator.ValidateArrayV5(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Titles)
                .NotNullOrEmptyIfRequired(nameof(StudyVersionDto.Titles), _requiredProperties)
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
                .SetValidator(new GovernanceDateValidator(_httpContextAccessor));

            RuleForEach(x => x.ReferenceIdentifiers)
                .SetValidator(new ReferenceIdentifierValidator(_httpContextAccessor));

            RuleForEach(x => x.Amendments)
                .SetValidator(new StudyAmendmentValidator(_httpContextAccessor));

            RuleForEach(x => x.StudyDesigns)
                .SetValidator(new StudyDesignValidator(_httpContextAccessor));

            RuleForEach(x => x.StudyIdentifiers)
                .SetValidator(new StudyIdentifierValidator(_httpContextAccessor));

            RuleForEach(x => x.StudyInterventions)
                .SetValidator(new StudyInterventionValidator(_httpContextAccessor));

            RuleForEach(x => x.Titles)
                .SetValidator(new StudyTitleValidator(_httpContextAccessor));
        }
    }
}
