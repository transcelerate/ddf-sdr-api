using FluentValidation;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine.Common
{
    public class SearchTitleParametersValidator : AbstractValidator<SearchTitleParametersDto>
    {
        public SearchTitleParametersValidator()
        {
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
