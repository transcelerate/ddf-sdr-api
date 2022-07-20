using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.StudyV1;

namespace TransCelerate.SDR.RuleEngineV1
{
    public static class ValidationDependenciesV1
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependenciesV1(this IServiceCollection services)
        {
            //Search Validators
            services.AddTransient<IValidator<SearchParametersDto>, SearchParametersValidator>();
            

            return services;
        }
    }
}
