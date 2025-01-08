using FluentValidation;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.Linq;

namespace TransCelerate.SDR.RuleEngine.Common
{
    public class SearchParametersValidator : AbstractValidator<SearchParametersDto>
    {
        public SearchParametersValidator()
        {
            RuleFor(x => x.UsdmVersion)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)                
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x => ApiUsdmVersionMapping.SDRVersions.SelectMany(x=>x.UsdmVersions).Any(y=>y==x))
                .WithMessage(Constants.ErrorMessages.InvalidUsdmVersion)
                .When(x => x.ValidateUsdmVersion, ApplyConditionTo.AllValidators);

            RuleFor(x => x.FromDate)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);

            RuleFor(x => x.ToDate)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);

            RuleFor(x => x.PageNumber)
                .Must(x => x > 0)
                .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);

            RuleFor(x => x.PageSize)
               .Must(x => x > 0)
               .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);
        }
    }
}
