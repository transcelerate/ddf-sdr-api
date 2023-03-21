using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class EpochValidator : AbstractValidator<EpochDTO>
    {
        /// <summary>
        /// Validator for Epoch
        /// </summary>
        public EpochValidator()
        {
            RuleFor(x => x.EpochType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

        }
    }
}
