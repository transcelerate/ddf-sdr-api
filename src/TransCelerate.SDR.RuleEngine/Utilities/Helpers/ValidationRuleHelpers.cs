using FluentValidation;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5.Utilities.Helpers
{
    /// <summary>
    /// Extension helper methods for validation rules
    /// </summary>
    public static class ValidationRuleHelpers
    {
        /// <summary>
        /// Applies NotNull and NotEmpty validation rules conditionally based on conformance rules
        /// </summary>
        /// <typeparam name="T">The type being validated</typeparam>
        /// <typeparam name="TProperty">The property type being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder</param>
        /// <param name="propertyName">The name of the property being validated</param>
        /// <param name="requiredProperties">Required properties for the class</param>
        /// <returns>Rule builder options for further chaining</returns>
        public static IRuleBuilderOptions<T, TProperty> NotNullOrEmptyIfRequired<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            string propertyName,
            HashSet<string> requiredProperties)
        {
            return ruleBuilder
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => requiredProperties.Contains(propertyName), ApplyConditionTo.AllValidators);
        }

        /// <summary>
        /// Validates that InstanceType matches the validator name, without the "Validator" suffix.
        /// </summary>
        public static IRuleBuilderOptions<T, string> MustMatchValidatorInstanceType<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            string validatorName)
        {
            var expectedInstanceType = validatorName.RemoveValidator();

            return ruleBuilder
                .Must(x => expectedInstanceType == x)
                .WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);
        }
    }
}