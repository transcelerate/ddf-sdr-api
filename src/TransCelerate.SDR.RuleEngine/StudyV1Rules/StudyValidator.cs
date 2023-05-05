using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for Study
    /// </summary>
    public class StudyValidator : AbstractValidator<StudyDto>
    {
        public StudyValidator()
        {
            RuleFor(x => x.ClinicalStudy)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.RootElementMissing)
                .SetValidator(new ClinicalStudyValidator());
        }
    }
}
