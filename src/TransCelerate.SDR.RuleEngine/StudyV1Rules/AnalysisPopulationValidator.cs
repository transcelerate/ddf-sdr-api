using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    public class AnalysisPopulationValidator : AbstractValidator<AnalysisPopulationDto>
    {
        public AnalysisPopulationValidator()
        {
            RuleFor(x => x.Population_desc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);    
        }
    }
}





