using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyProtocolValidator : AbstractValidator<StudyProtocolDTO>
    {
        /// <summary>
        /// Validator for studyProtocol
        /// </summary>
        public StudyProtocolValidator()
        {
        }
    }
}
