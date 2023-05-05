using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for Indication
    /// </summary>
    public class IndicationValidator : AbstractValidator<IndicationDto>
    {
        public IndicationValidator()
        {
            RuleFor(x => x.IndicationDesc)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.Codes)
                .ForEach(x => x.SetValidator(new CodeValidator()));
        }
    }
}





