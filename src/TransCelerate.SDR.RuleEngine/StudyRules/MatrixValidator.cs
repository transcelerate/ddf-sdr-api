using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class MatrixValidator : AbstractValidator<MatrixDTO>
    {
        /// <summary>
        /// Validator for Matrix
        /// </summary>
        public MatrixValidator()
        {
            RuleFor(x => x.Items)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .ForEach(y => y.SetValidator(new ItemValidator()));
        }
    }
}
