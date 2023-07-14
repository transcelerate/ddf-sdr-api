using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyCells
    /// </summary>
    public class StudyCellsValidator : AbstractValidator<StudyCellDto>
    {
        public StudyCellsValidator()
        {
            RuleFor(x => x.StudyArm)
                .SetValidator(new StudyArmValidator());

            RuleFor(x => x.StudyEpoch)
                .SetValidator(new StudyEpochValidator());

            RuleFor(x => x.StudyElements)
                .ForEach(x => x.SetValidator(new StudyElementsValidator()));
        }
    }
}





