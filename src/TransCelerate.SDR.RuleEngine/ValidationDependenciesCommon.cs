using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.Common;

namespace TransCelerate.SDR.RuleEngine.Common
{
    public static class ValidationDependenciesCommon
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependenciesCommon(this IServiceCollection services)
        {
            // Validators            
            services.AddTransient<IValidator<SearchParametersDto>, SearchParametersValidator>();

            return services;
        }
    }
}
