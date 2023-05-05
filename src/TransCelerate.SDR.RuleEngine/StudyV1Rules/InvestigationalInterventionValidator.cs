using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for InvestigationalIntervention
    /// </summary>
    public class InvestigationalInterventionValidator : AbstractValidator<InvestigationalInterventionDto>
    {
        public InvestigationalInterventionValidator()
        {
            RuleFor(x => x.InterventionDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.Codes)
                .ForEach(x => x.SetValidator(new CodeValidator()));
        }
    }
}





