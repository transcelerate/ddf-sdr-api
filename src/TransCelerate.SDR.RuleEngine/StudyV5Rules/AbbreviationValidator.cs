using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.RuleEngineV5.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine.StudyV5Rules
{
    /// <summary>
    /// This class is the validator for Abbreviation
    /// </summary>
    public class AbbreviationValidator : AbstractValidator<AbbreviationDto>
    {
        private readonly HashSet<string> _requiredProperties = new()
        {
            nameof(AbbreviationDto.Id),
            nameof(AbbreviationDto.InstanceType),
            nameof(AbbreviationDto.AbbreviatedText),
            nameof(AbbreviationDto.ExpandedText)
        };

        public AbbreviationValidator()
        {
            RuleFor(x => x.Id)
              .NotNullOrEmptyIfRequired(nameof(AbbreviationDto.Id), _requiredProperties);

            RuleFor(x => x.InstanceType)
              .NotNullOrEmptyIfRequired(nameof(AbbreviationDto.InstanceType), _requiredProperties)
              .MustMatchValidatorInstanceType(this.GetType().Name);

            RuleFor(x => x.AbbreviatedText)
               .NotNullOrEmptyIfRequired(nameof(AbbreviationDto.AbbreviatedText), _requiredProperties);

            RuleFor(x => x.ExpandedText)
               .NotNullOrEmptyIfRequired(nameof(AbbreviationDto.ExpandedText), _requiredProperties);

            RuleFor(x => x.Notes)
               .NotNullOrEmptyIfRequired(nameof(AbbreviationDto.Notes), _requiredProperties);
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
