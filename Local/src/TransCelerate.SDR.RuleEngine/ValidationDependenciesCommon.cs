using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;

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
            services.AddTransient<IValidator<SearchTitleParametersDto>, SearchTitleParametersValidator>();


            //User Group Mapping Validator
            services.AddTransient<IValidator<SDRGroupsDTO>, GroupsValidator>();
            services.AddTransient<IValidator<PostUserToGroupsDTO>, PostUserToGroupValidator>();
            services.AddTransient<IValidator<UserGroupsQueryParameters>, UserGroupsQueryParametersValidator>();
            services.AddTransient<IValidator<GroupFilterDTO>, GroupFilterValidator>();
            services.AddTransient<IValidator<GroupFilterValuesDTO>, GroupFilterValuesValidator>();

            //Token Validator
            services.AddTransient<IValidator<UserLogin>, UserLoginValidator>();

            return services;
        }
    }
}
