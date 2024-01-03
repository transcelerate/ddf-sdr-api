using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.StudyV4;

namespace TransCelerate.SDR.RuleEngineV4
{
    public static class ValidationDependenciesV4
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependenciesV4(this IServiceCollection services)
        {
            // Validators
            services.AddTransient<IValidator<StudyDto>, StudyValidator>();
            services.AddTransient<IValidator<ActivityDto>, ActivityValidator>();
            services.AddTransient<IValidator<AddressDto>, AddressValidator>();
            services.AddTransient<IValidator<AdministrationDurationDto>, AdministrationDurationValidator>();
            services.AddTransient<IValidator<AgentAdministrationDto>, AgentAdministrationValidator>();
            services.AddTransient<IValidator<AliasCodeDto>, AliasCodeValidator>();
            services.AddTransient<IValidator<AnalysisPopulationDto>, AnalysisPopulationValidator>();
            services.AddTransient<IValidator<BiomedicalConceptDto>, BiomedicalConceptValidator>();
            services.AddTransient<IValidator<BiomedicalConceptCategoryDto>, BiomedicalConceptCategoryValidator>();
            services.AddTransient<IValidator<BiomedicalConceptPropertyDto>, BiomedicalConceptPropertyValidator>();
            services.AddTransient<IValidator<BiomedicalConceptSurrogateDto>, BiomedicalConceptSurrogateValidator>();
            services.AddTransient<IValidator<CharacteristicDto>, CharacteristicValidator>();
            services.AddTransient<IValidator<CodeDto>, CodeValidator>();
            services.AddTransient<IValidator<EligibilityCriteriaDto>, EligibilityCriteriaValidator>();
            services.AddTransient<IValidator<EncounterDto>, EncounterValidator>();
            services.AddTransient<IValidator<EndpointDto>, EndpointValidator>();
            services.AddTransient<IValidator<EstimandDto>, EstimandValidator>();
            services.AddTransient<IValidator<GeographicScopeDto>, GeographicScopeValidator>();
            services.AddTransient<IValidator<GovernanceDateDto>, GovernanceDateValidator>();
            services.AddTransient<IValidator<IndicationDto>, IndicationValidator>();
            services.AddTransient<IValidator<InterCurrentEventDto>, InterCurrentEventValidator>();
            services.AddTransient<IValidator<NarrativeContentDto>, NarrativeContentValidator>();
            services.AddTransient<IValidator<ObjectiveDto>, ObjectiveValidator>();
            services.AddTransient<IValidator<OrganisationDto>, OrganisationValidator>();
            services.AddTransient<IValidator<ProcedureDto>, ProcedureValidator>();
            services.AddTransient<IValidator<QuantityDto>, QuantityValidator>();
            services.AddTransient<IValidator<RangeDto>, RangeValidator>();
            services.AddTransient<IValidator<ResponseCodeDto>, ResponseCodeValidator>();
            services.AddTransient<IValidator<ScheduleTimelineDto>, ScheduleTimelinesValidator>();
            services.AddTransient<IValidator<ScheduledInstanceDto>, ScheduledInstanceValidator>();
            services.AddTransient<IValidator<ScheduledDecisionInstanceDto>, ScheduledDecisionInstanceValidator>();
            services.AddTransient<IValidator<ScheduledActivityInstanceDto>, ScheduledActivityInstanceValidator>();
            services.AddTransient<IValidator<ScheduleTimelineExitDto>, ScheduleTimelineExitValidator>();
            services.AddTransient<IValidator<StudyAmendmentDto>, StudyAmendmentValidator>();
            services.AddTransient<IValidator<StudyAmendmentReasonDto>, StudyAmendmentReasonValidator>();
            services.AddTransient<IValidator<StudyArmDto>, StudyArmValidator>();
            services.AddTransient<IValidator<StudyCellDto>, StudyCellsValidator>();
            services.AddTransient<IValidator<StudyCohortDto>, StudyCohortValidator>();
            services.AddTransient<IValidator<StudyDefinitionsDto>, StudyDefinitionsValidator>();
            services.AddTransient<IValidator<StudyDesignDto>, StudyDesignValidator>();
            services.AddTransient<IValidator<StudyDesignPopulationDto>, StudyDesignPopulationValidator>();
            services.AddTransient<IValidator<StudyElementDto>, StudyElementsValidator>();
            services.AddTransient<IValidator<StudyEpochDto>, StudyEpochValidator>();
            services.AddTransient<IValidator<StudyInterventionDto>, StudyInterventionValidator>();
            services.AddTransient<IValidator<StudyIdentifierDto>, StudyIdentifierValidator>();
            services.AddTransient<IValidator<StudyProtocolDocumentVersionDto>, StudyProtocolDocumentVersionValidator>();
            services.AddTransient<IValidator<StudyProtocolDocumentDto>, StudyProtocolDocumentValidator>();
            services.AddTransient<IValidator<StudyVersionDto>, StudyVersionValidator>();
            services.AddTransient<IValidator<SyntaxTemplateDictionaryDto>, SyntaxTemplateDictionaryValidator>();
            services.AddTransient<IValidator<TransitionRuleDto>, TransitionRuleValidator>();
            services.AddTransient<IValidator<TimingDto>, TimingValidator>();

            return services;
        }
    }
}
