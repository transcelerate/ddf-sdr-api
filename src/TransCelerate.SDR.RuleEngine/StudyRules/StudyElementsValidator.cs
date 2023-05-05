using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyElementsValidator : AbstractValidator<StudyElementDTO>
    {
        /// <summary>
        /// Validator for StudyElemnts
        /// </summary>
        public StudyElementsValidator()
        {
            RuleFor(x => x.EndRule)
                .SetValidator(new RuleValidator());

            RuleFor(x => x.StartRule)
                .SetValidator(new RuleValidator());
        }
    }
}
