using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for Study
    /// </summary>
    public class StudyDefinitionsValidator : AbstractValidator<StudyDefinitionsDto>
    {
        public StudyDefinitionsValidator()
        {
            RuleFor(x => x.Study)
                .Cascade(CascadeMode.Stop)
                .NotNull().OverridePropertyName(IdFieldPropertyName.ParentElement.ClinicalStudy).WithName(IdFieldPropertyName.ParentElement.ClinicalStudy).WithMessage(Constants.ValidationErrorMessage.RootElementMissing)
                .SetValidator(new StudyValidator());
        }
    }
}
