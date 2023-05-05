using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;

namespace TransCelerate.SDR.RuleEngine
{
    public static class ValidationDependencies
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
        {
            //Study Level Validators
            services.AddTransient<IValidator<ClinicalStudyDTO>, ClinicalStudyValidator>();
            services.AddTransient<IValidator<PostStudyDTO>, PostStudyValidator>();
            services.AddTransient<IValidator<StudyIdentifierDTO>, StudyIdentifiersValidator>();
            services.AddTransient<IValidator<StudyProtocolDTO>, StudyProtocolValidator>();
            services.AddTransient<IValidator<CurrentSectionsDTO>, CurrenSectionsValidator>();
            services.AddTransient<IValidator<CurrentSectionsDTO>, CurrentSectionsForDesignValidator>();
            services.AddTransient<IValidator<StudyDesignDTO>, StudyDesignValidator>();
            //Section Level Validators
            ////Investigational Intervention Section
            services.AddTransient<IValidator<InvestigationalInterventionDTO>, InvestigationalInterventionValidatior>();
            ////Study Objectives Section
            services.AddTransient<IValidator<StudyObjectiveDTO>, StudyObjectivesValidator>();
            ////Study Indications Section
            services.AddTransient<IValidator<StudyIndicationDTO>, StudyIndicationValidator>();
            ////Study Design Section
            //////Study Population Section
            services.AddTransient<IValidator<StudyPopulationDTO>, StudyPopulationValidator>();
            //////Study Cell Section
            services.AddTransient<IValidator<StudyCellDTO>, StudyCellsValidator>();
            services.AddTransient<IValidator<StudyElementDTO>, StudyElementsValidator>();
            services.AddTransient<IValidator<StudyArmDTO>, StudyArmValidator>();
            services.AddTransient<IValidator<StudyEpochDTO>, StudyEpochValidator>();
            //////Planned WorkFlow Section
            services.AddTransient<IValidator<PlannedWorkflowDTO>, PlannedWorkFlowValidator>();
            services.AddTransient<IValidator<WorkflowItemMatrixDTO>, WorkFlowItemMatrixValidator>();
            services.AddTransient<IValidator<MatrixDTO>, MatrixValidator>();
            services.AddTransient<IValidator<ItemDTO>, ItemValidator>();
            services.AddTransient<IValidator<ActivityDTO>, ActivityValidator>();
            services.AddTransient<IValidator<DefinedProcedureDTO>, DefinedProcedureValidator>();
            services.AddTransient<IValidator<StudyDataCollectionDTO>, StudyDataCollectionValidator>();
            services.AddTransient<IValidator<EncounterDTO>, EncounterValidator>();

            //Common Section
            services.AddTransient<IValidator<CodingDTO>, CodingValidator>();
            services.AddTransient<IValidator<RuleDTO>, RuleValidator>();
            services.AddTransient<IValidator<EndpointsDTO>, EndpointsValidator>();
            services.AddTransient<IValidator<PointInTimeDTO>, PointInTimeValidator>();
            services.AddTransient<IValidator<EpochDTO>, EpochValidator>();

            //Search Validaitions
            services.AddTransient<IValidator<SearchParametersDTO>, SearchParametersValidator>();


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
