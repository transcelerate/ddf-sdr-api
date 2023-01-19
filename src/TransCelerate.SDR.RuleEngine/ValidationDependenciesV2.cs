﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.StudyV2;

namespace TransCelerate.SDR.RuleEngineV2
{
    public static class ValidationDependenciesV2
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependenciesV2(this IServiceCollection services)
        {
            // Validators            
            services.AddTransient<IValidator<ActivityDto>, ActivityValidator>();
            services.AddTransient<IValidator<AnalysisPopulationDto>, AnalysisPopulationValidator>();
            services.AddTransient<IValidator<ClinicalStudyDto>, ClinicalStudyValidator>();
            services.AddTransient<IValidator<CodeDto>, CodeValidator>();
            services.AddTransient<IValidator<ProcedureDto>, ProcedureValidator>();
            services.AddTransient<IValidator<EncounterDto>, EncounterValidator>();
            services.AddTransient<IValidator<EndpointDto>, EndpointValidator>();
            services.AddTransient<IValidator<EstimandDto>, StudyEstimandsValidator>();
            services.AddTransient<IValidator<IndicationDto>, IndicationValidator>();
            services.AddTransient<IValidator<InterCurrentEventDto>, InterCurrentEventsValidator>();
            services.AddTransient<IValidator<InvestigationalInterventionDto>, InvestigationalInterventionValidator>();
            services.AddTransient<IValidator<ObjectiveDto>, ObjectiveValidator>();            
            services.AddTransient<IValidator<StudyArmDto>, StudyArmValidator>();
            services.AddTransient<IValidator<StudyCellDto>, StudyCellsValidator>();
            services.AddTransient<IValidator<StudyDataDto>, StudyDataCollectionValidator>();
            services.AddTransient<IValidator<StudyDesignDto>, StudyDesignValidator>();
            services.AddTransient<IValidator<StudyDesignPopulationDto>, StudyDesignPopulationValidator>();
            services.AddTransient<IValidator<StudyDto>, StudyValidator>();
            services.AddTransient<IValidator<StudyElementDto>, StudyElementsValidator>();
            services.AddTransient<IValidator<StudyEpochDto>, StudyEpochValidator>();
            services.AddTransient<IValidator<StudyIdentifierDto>, StudyIdentifiersValidator>();
            services.AddTransient<IValidator<OrganisationDto>, OrganisationValidator>();
            services.AddTransient<IValidator<StudyProtocolVersionDto>, StudyProtocolVersionsValidator>();
            services.AddTransient<IValidator<TransitionRuleDto>, TransitionRuleValidator>();
            services.AddTransient<IValidator<WorkflowDto>, WorkflowValidator>();
            services.AddTransient<IValidator<WorkflowItemDto>, WorkflowItemValidator>();
            services.AddTransient<IValidator<AliasCodeDto>, AliasCodeValidator>();
            services.AddTransient<IValidator<AddressDto>, AddressValidator>();

            return services;
        }
    }
}
