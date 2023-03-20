using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class DefinedProcedureValidator : AbstractValidator<DefinedProcedureDTO>
    {
        /// <summary>
        /// Validator for DefinedProcedure
        /// </summary>
        public DefinedProcedureValidator()
        {
            RuleFor(x => x.ProcedureCode)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}
