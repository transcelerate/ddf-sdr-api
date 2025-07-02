using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO.StudyV5;

namespace TransCelerate.SDR.RuleEngineV5
{
    public static class ValidationDependenciesV5
    {
        /// <summary>
        /// Add all the dependencies for validations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationDependenciesV5(this IServiceCollection services)
        {
            // Validators
            services.AddTransient<IValidator<StudyDto>, StudyValidator>();
            services.AddTransient<IValidator<ActivityDto>, ActivityValidator>();
            services.AddTransient<IValidator<AddressDto>, AddressValidator>();
            services.AddTransient<IValidator<AdministrableProductIdentifierDto>, AdministrableProductIdentifierValidator>();
            services.AddTransient<IValidator<AdministrableProductPropertyDto>, AdministrableProductPropertyValidator>();
            services.AddTransient<IValidator<AdministrableProductDto>, AdministrableProductValidator>();
            services.AddTransient<IValidator<AdministrationDto>, AdministrationValidator>();
            services.AddTransient<IValidator<AdministrationDurationDto>, AdministrationDurationValidator>();
            services.AddTransient<IValidator<AliasCodeDto>, AliasCodeValidator>();
            services.AddTransient<IValidator<AnalysisPopulationDto>, AnalysisPopulationValidator>();
            services.AddTransient<IValidator<AssignedPersonDto>, AssignedPersonValidator>();
            services.AddTransient<IValidator<BiomedicalConceptDto>, BiomedicalConceptValidator>();
            services.AddTransient<IValidator<BiomedicalConceptCategoryDto>, BiomedicalConceptCategoryValidator>();
            services.AddTransient<IValidator<BiomedicalConceptPropertyDto>, BiomedicalConceptPropertyValidator>();
            services.AddTransient<IValidator<BiomedicalConceptSurrogateDto>, BiomedicalConceptSurrogateValidator>();
            services.AddTransient<IValidator<CharacteristicDto>, CharacteristicValidator>();
            services.AddTransient<IValidator<CodeDto>, CodeValidator>();
			services.AddTransient<IValidator<CommentAnnotationDto>, CommentAnnotationValidator>();
			services.AddTransient<IValidator<DocumentContentReferenceDto>, DocumentContentReferenceValidator>();
			services.AddTransient<IValidator<EligibilityCriterionDto>, EligibilityCriterionValidator>();
			services.AddTransient<IValidator<EligibilityCriterionItemDto>, EligibilityCriterionItemValidator>();
            services.AddTransient<IValidator<EncounterDto>, EncounterValidator>();
            services.AddTransient<IValidator<EndpointDto>, EndpointValidator>();
            services.AddTransient<IValidator<EstimandDto>, EstimandValidator>();
            services.AddTransient<IValidator<GeographicScopeDto>, GeographicScopeValidator>();
            services.AddTransient<IValidator<GovernanceDateDto>, GovernanceDateValidator>();
            services.AddTransient<IValidator<IdentifierDto>, IdentifierValidator>();
            services.AddTransient<IValidator<IndicationDto>, IndicationValidator>();
            services.AddTransient<IValidator<IngredientDto>, IngredientValidator>();
            services.AddTransient<IValidator<IntercurrentEventDto>, IntercurrentEventValidator>();
            services.AddTransient<IValidator<NarrativeContentDto>, NarrativeContentValidator>();
            services.AddTransient<IValidator<ObjectiveDto>, ObjectiveValidator>();
            services.AddTransient<IValidator<OrganizationDto>, OrganizationValidator>();
            services.AddTransient<IValidator<ProcedureDto>, ProcedureValidator>();
            services.AddTransient<IValidator<QuantityDto>, QuantityValidator>();
            services.AddTransient<IValidator<RangeDto>, RangeValidator>();
            services.AddTransient<IValidator<ReferenceIdentifierDto>, ReferenceIdentifierValidator>();
            services.AddTransient<IValidator<ResponseCodeDto>, ResponseCodeValidator>();
            services.AddTransient<IValidator<ScheduleTimelineDto>, ScheduleTimelineValidator>();
            services.AddTransient<IValidator<ScheduledInstanceDto>, ScheduledInstanceValidator>();
            services.AddTransient<IValidator<ScheduledDecisionInstanceDto>, ScheduledDecisionInstanceValidator>();
            services.AddTransient<IValidator<ScheduledActivityInstanceDto>, ScheduledActivityInstanceValidator>();
            services.AddTransient<IValidator<ScheduleTimelineExitDto>, ScheduleTimelineExitValidator>();
            services.AddTransient<IValidator<StrengthDto>, StrengthValidator>();
            services.AddTransient<IValidator<StudyAmendmentDto>, StudyAmendmentValidator>();
            services.AddTransient<IValidator<StudyAmendmentImpactDto>, StudyAmendmentImpactValidator>();
            services.AddTransient<IValidator<StudyAmendmentReasonDto>, StudyAmendmentReasonValidator>();
            services.AddTransient<IValidator<StudyArmDto>, StudyArmValidator>();
            services.AddTransient<IValidator<StudyChangeDto>, StudyChangeValidator>();
            services.AddTransient<IValidator<StudyCellDto>, StudyCellValidator>();
            services.AddTransient<IValidator<StudyCohortDto>, StudyCohortValidator>();
            services.AddTransient<IValidator<StudyDefinitionsDto>, StudyDefinitionsValidator>();
            services.AddTransient<IValidator<StudyDesignDto>, StudyDesignValidator>();
            services.AddTransient<IValidator<StudyDesignPopulationDto>, StudyDesignPopulationValidator>();
            services.AddTransient<IValidator<StudyElementDto>, StudyElementValidator>();
            services.AddTransient<IValidator<StudyEpochDto>, StudyEpochValidator>();
            services.AddTransient<IValidator<StudyInterventionDto>, StudyInterventionValidator>();
            services.AddTransient<IValidator<StudyIdentifierDto>, StudyIdentifierValidator>();			
			services.AddTransient<IValidator<StudyDefinitionDocumentVersionDto>, StudyDefinitionDocumentVersionValidator>();
			services.AddTransient<IValidator<StudyDefinitionDocumentDto>, StudyDefinitionDocumentValidator>();
            services.AddTransient<IValidator<StudyRoleDto>, StudyRoleValidator>();
			services.AddTransient<IValidator<StudyVersionDto>, StudyVersionValidator>();
            services.AddTransient<IValidator<SubstanceDto>, SubstanceValidator>();
            services.AddTransient<IValidator<NarrativeContentItemDto>, NarrativeContentItemValidator>();
            services.AddTransient<IValidator<SyntaxTemplateDictionaryDto>, SyntaxTemplateDictionaryValidator>();
            services.AddTransient<IValidator<TransitionRuleDto>, TransitionRuleValidator>();
            services.AddTransient<IValidator<TimingDto>, TimingValidator>();

            return services;
        }
    }
}
