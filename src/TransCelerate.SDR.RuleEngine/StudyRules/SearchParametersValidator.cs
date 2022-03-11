using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine
{
    public class SearchParametersValidator : AbstractValidator<SearchParametersDTO>
    {
        /// <summary>
        /// Validator for SearchParameters
        /// </summary>
        public SearchParametersValidator()
        {
            RuleFor(x => x.studyTitle)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);
            RuleFor(x => x.studyId)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);
            RuleFor(x => x.indication)
                .Matches(Constants.RegularExpressions.AlphaNumericsWithSpace)
                .WithMessage(Constants.ValidationErrorMessage.AlphaNumericErrorMessage);           
            RuleFor(x => x.fromDate)
                .Must(x=>DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x => x.toDate)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x=>x.pageNumber)
                .Must(x=>x>0)
                .WithMessage(Constants.ValidationErrorMessage.NumberError);
            RuleFor(x => x.pageSize)
               .Must(x => x > 0)
               .WithMessage(Constants.ValidationErrorMessage.NumberError);
        }
    }
}
