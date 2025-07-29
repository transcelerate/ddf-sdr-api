using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine.StudyV5Rules
{
    /// <summary>
    /// This class is the validator for Abbreviation
    /// </summary>
    public class AbbreviationValidator : AbstractValidator<AbbreviationDto>
    {
        private readonly HashSet<string> _requiredFields = new()
        {
            nameof(AbbreviationDto.Id),
            nameof(AbbreviationDto.InstanceType),
            nameof(AbbreviationDto.AbbreviatedText),
            nameof(AbbreviationDto.ExpandedText),
        };

        public AbbreviationValidator()
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(AbbreviationDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => _requiredFields.Contains(nameof(AbbreviationDto.InstanceType)), ApplyConditionTo.AllValidators)
              .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.AbbreviatedText)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(AbbreviationDto.AbbreviatedText)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ExpandedText)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(AbbreviationDto.ExpandedText)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Notes)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => _requiredFields.Contains(nameof(AbbreviationDto.Notes)), ApplyConditionTo.AllValidators);
        }

        public static bool ValidateAbbreviatedText(List<AbbreviationDto> arrayElement)
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                var abbreviatedTextArray = arrayElement.Select(x => x.AbbreviatedText).ToList();
                abbreviatedTextArray.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return abbreviatedTextArray.Distinct().Count() == abbreviatedTextArray.Count;
            }
            return true;
        }

        public static bool ValidateExpandedText(List<AbbreviationDto> arrayElement)
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                var expandedTextArray = arrayElement.Select(x => x.ExpandedText).ToList();
                expandedTextArray.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return expandedTextArray.Distinct().Count() == expandedTextArray.Count;
            }
            return true;
        }
    }
}
