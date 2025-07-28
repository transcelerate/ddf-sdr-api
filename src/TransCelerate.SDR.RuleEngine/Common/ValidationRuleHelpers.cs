using FluentValidation;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5.Common
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
        /// <param name="usdmVersion">The USDM version from headers</param>
        /// <param name="validatorClassName">The name of the validator class</param>
        /// <param name="propertyName">The name of the property being validated</param>
        /// <returns>Rule builder options for further chaining</returns>
        public static IRuleBuilderOptions<T, TProperty> NotNullOrEmptyIfRequired<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            string usdmVersion,
            string validatorClassName,
            string propertyName)
        {
            return ruleBuilder
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(usdmVersion, validatorClassName, propertyName), ApplyConditionTo.AllValidators);
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