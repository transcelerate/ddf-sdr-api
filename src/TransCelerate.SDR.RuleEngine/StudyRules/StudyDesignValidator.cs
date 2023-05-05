using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyDesignValidator : AbstractValidator<StudyDesignDTO>
    {
        /// <summary>
        /// Validator for studyProtocol
        /// </summary>
        public StudyDesignValidator()
        {
            RuleFor(x => x.CurrentSections)
                .ForEach(y => y.SetValidator(new CurrentSectionsForDesignValidator()));
        }
    }
}
