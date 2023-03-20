using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyDesignPopulation
    /// </summary>
    public class StudyDesignPopulationValidator : AbstractValidator<StudyDesignPopulationDto>
    {
        public StudyDesignPopulationValidator()
        {
            RuleFor(x => x.PopulationDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}





