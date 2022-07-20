using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngineV1
{
    public class SearchParametersValidator : AbstractValidator<SearchParametersDto>
    {
        /// <summary>
        /// Validator for SearchParameters
        /// </summary>
        public SearchParametersValidator()
        {
            RuleFor(x => x.StudyTitle)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);
            RuleFor(x => x.StudyId)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);
            RuleFor(x => x.Indication)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);           
            RuleFor(x => x.FromDate)
                .Must(x=>DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x => x.ToDate)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x=>x.PageNumber)
                .Must(x=> x > 0)
                .WithMessage(Constants.ValidationErrorMessage.NumberError);
            RuleFor(x => x.PageSize)
               .Must(x => x > 0)
               .WithMessage(Constants.ValidationErrorMessage.NumberError);
        }
    }
}
