<a name='assembly'></a>
# TransCelerate.SDR.RuleEngine

## Contents

- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV2-ActivityValidator 'TransCelerate.SDR.RuleEngineV2.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV3-ActivityValidator 'TransCelerate.SDR.RuleEngineV3.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV4-ActivityValidator 'TransCelerate.SDR.RuleEngineV4.ActivityValidator')
- [AddressValidator](#T-TransCelerate-SDR-RuleEngineV2-AddressValidator 'TransCelerate.SDR.RuleEngineV2.AddressValidator')
- [AddressValidator](#T-TransCelerate-SDR-RuleEngineV3-AddressValidator 'TransCelerate.SDR.RuleEngineV3.AddressValidator')
- [AddressValidator](#T-TransCelerate-SDR-RuleEngineV4-AddressValidator 'TransCelerate.SDR.RuleEngineV4.AddressValidator')
- [AdministrationDurationValidator](#T-TransCelerate-SDR-RuleEngineV4-AdministrationDurationValidator 'TransCelerate.SDR.RuleEngineV4.AdministrationDurationValidator')
- [AgentAdministrationValidator](#T-TransCelerate-SDR-RuleEngineV4-AgentAdministrationValidator 'TransCelerate.SDR.RuleEngineV4.AgentAdministrationValidator')
- [AliasCodeValidator](#T-TransCelerate-SDR-RuleEngineV2-AliasCodeValidator 'TransCelerate.SDR.RuleEngineV2.AliasCodeValidator')
- [AliasCodeValidator](#T-TransCelerate-SDR-RuleEngineV3-AliasCodeValidator 'TransCelerate.SDR.RuleEngineV3.AliasCodeValidator')
- [AliasCodeValidator](#T-TransCelerate-SDR-RuleEngineV4-AliasCodeValidator 'TransCelerate.SDR.RuleEngineV4.AliasCodeValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV2.AnalysisPopulationValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV3-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV3.AnalysisPopulationValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV4-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV4.AnalysisPopulationValidator')
- [BiomedicalConceptCategoryValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptCategoryValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptCategoryValidator')
- [BiomedicalConceptCategoryValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptCategoryValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptCategoryValidator')
- [BiomedicalConceptCategoryValidator](#T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptCategoryValidator 'TransCelerate.SDR.RuleEngineV4.BiomedicalConceptCategoryValidator')
- [BiomedicalConceptPropertyValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptPropertyValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptPropertyValidator')
- [BiomedicalConceptPropertyValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptPropertyValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptPropertyValidator')
- [BiomedicalConceptPropertyValidator](#T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptPropertyValidator 'TransCelerate.SDR.RuleEngineV4.BiomedicalConceptPropertyValidator')
- [BiomedicalConceptSurrogateValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptSurrogateValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptSurrogateValidator')
- [BiomedicalConceptSurrogateValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptSurrogateValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptSurrogateValidator')
- [BiomedicalConceptSurrogateValidator](#T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptSurrogateValidator 'TransCelerate.SDR.RuleEngineV4.BiomedicalConceptSurrogateValidator')
- [BiomedicalConceptValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptValidator')
- [BiomedicalConceptValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptValidator')
- [BiomedicalConceptValidator](#T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptValidator 'TransCelerate.SDR.RuleEngineV4.BiomedicalConceptValidator')
- [CharacteristicValidator](#T-TransCelerate-SDR-RuleEngineV4-CharacteristicValidator 'TransCelerate.SDR.RuleEngineV4.CharacteristicValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV2-CodeValidator 'TransCelerate.SDR.RuleEngineV2.CodeValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV3-CodeValidator 'TransCelerate.SDR.RuleEngineV3.CodeValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV4-CodeValidator 'TransCelerate.SDR.RuleEngineV4.CodeValidator')
- [ConditionAssignmentValidator](#T-TransCelerate-SDR-RuleEngineV4-ConditionAssignmentValidator 'TransCelerate.SDR.RuleEngineV4.ConditionAssignmentValidator')
- [ConditionValidator](#T-TransCelerate-SDR-RuleEngineV4-ConditionValidator 'TransCelerate.SDR.RuleEngineV4.ConditionValidator')
- [EligibilityCriterionValidator](#T-TransCelerate-SDR-RuleEngineV4-EligibilityCriterionValidator 'TransCelerate.SDR.RuleEngineV4.EligibilityCriterionValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV2-EncounterValidator 'TransCelerate.SDR.RuleEngineV2.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV3-EncounterValidator 'TransCelerate.SDR.RuleEngineV3.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV4-EncounterValidator 'TransCelerate.SDR.RuleEngineV4.EncounterValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV2-EndpointValidator 'TransCelerate.SDR.RuleEngineV2.EndpointValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV3-EndpointValidator 'TransCelerate.SDR.RuleEngineV3.EndpointValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV4-EndpointValidator 'TransCelerate.SDR.RuleEngineV4.EndpointValidator')
- [EstimandValidator](#T-TransCelerate-SDR-RuleEngineV4-EstimandValidator 'TransCelerate.SDR.RuleEngineV4.EstimandValidator')
- [GeographicScopeValidator](#T-TransCelerate-SDR-RuleEngineV4-GeographicScopeValidator 'TransCelerate.SDR.RuleEngineV4.GeographicScopeValidator')
- [GovernanceDateValidator](#T-TransCelerate-SDR-RuleEngineV4-GovernanceDateValidator 'TransCelerate.SDR.RuleEngineV4.GovernanceDateValidator')
- [GroupFilterValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValidator')
- [GroupFilterValuesValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValuesValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValuesValidator')
- [GroupsValidator](#T-TransCelerate-SDR-RuleEngine-GroupsValidator 'TransCelerate.SDR.RuleEngine.GroupsValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV2-IndicationValidator 'TransCelerate.SDR.RuleEngineV2.IndicationValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV3-IndicationValidator 'TransCelerate.SDR.RuleEngineV3.IndicationValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV4-IndicationValidator 'TransCelerate.SDR.RuleEngineV4.IndicationValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV2-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV2.InterCurrentEventsValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV3-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV3.InterCurrentEventsValidator')
- [IntercurrentEventValidator](#T-TransCelerate-SDR-RuleEngineV4-IntercurrentEventValidator 'TransCelerate.SDR.RuleEngineV4.IntercurrentEventValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV2-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV2.InvestigationalInterventionValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV3-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV3.InvestigationalInterventionValidator')
- [MaskingValidator](#T-TransCelerate-SDR-RuleEngineV4-MaskingValidator 'TransCelerate.SDR.RuleEngineV4.MaskingValidator')
- [NarrativeContentValidator](#T-TransCelerate-SDR-RuleEngineV4-NarrativeContentValidator 'TransCelerate.SDR.RuleEngineV4.NarrativeContentValidator')
- [ObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV2-ObjectiveValidator 'TransCelerate.SDR.RuleEngineV2.ObjectiveValidator')
- [ObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV3-ObjectiveValidator 'TransCelerate.SDR.RuleEngineV3.ObjectiveValidator')
- [ObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV4-ObjectiveValidator 'TransCelerate.SDR.RuleEngineV4.ObjectiveValidator')
- [OrganisationValidator](#T-TransCelerate-SDR-RuleEngineV2-OrganisationValidator 'TransCelerate.SDR.RuleEngineV2.OrganisationValidator')
- [OrganisationValidator](#T-TransCelerate-SDR-RuleEngineV3-OrganisationValidator 'TransCelerate.SDR.RuleEngineV3.OrganisationValidator')
- [OrganizationValidator](#T-TransCelerate-SDR-RuleEngineV4-OrganizationValidator 'TransCelerate.SDR.RuleEngineV4.OrganizationValidator')
- [ParameterMapValidator](#T-TransCelerate-SDR-RuleEngineV4-ParameterMapValidator 'TransCelerate.SDR.RuleEngineV4.ParameterMapValidator')
- [PostUserToGroupValidator](#T-TransCelerate-SDR-RuleEngine-PostUserToGroupValidator 'TransCelerate.SDR.RuleEngine.PostUserToGroupValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV2-ProcedureValidator 'TransCelerate.SDR.RuleEngineV2.ProcedureValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV3-ProcedureValidator 'TransCelerate.SDR.RuleEngineV3.ProcedureValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV4-ProcedureValidator 'TransCelerate.SDR.RuleEngineV4.ProcedureValidator')
- [QuantityValidator](#T-TransCelerate-SDR-RuleEngineV4-QuantityValidator 'TransCelerate.SDR.RuleEngineV4.QuantityValidator')
- [RangeValidator](#T-TransCelerate-SDR-RuleEngineV4-RangeValidator 'TransCelerate.SDR.RuleEngineV4.RangeValidator')
- [ResearchOrganizationValidator](#T-TransCelerate-SDR-RuleEngineV4-ResearchOrganizationValidator 'TransCelerate.SDR.RuleEngineV4.ResearchOrganizationValidator')
- [ResponseCodeValidator](#T-TransCelerate-SDR-RuleEngineV2-ResponseCodeValidator 'TransCelerate.SDR.RuleEngineV2.ResponseCodeValidator')
- [ResponseCodeValidator](#T-TransCelerate-SDR-RuleEngineV3-ResponseCodeValidator 'TransCelerate.SDR.RuleEngineV3.ResponseCodeValidator')
- [ResponseCodeValidator](#T-TransCelerate-SDR-RuleEngineV4-ResponseCodeValidator 'TransCelerate.SDR.RuleEngineV4.ResponseCodeValidator')
- [StudyAmendmentReasonValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyAmendmentReasonValidator 'TransCelerate.SDR.RuleEngineV4.StudyAmendmentReasonValidator')
- [StudyAmendmentValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyAmendmentValidator 'TransCelerate.SDR.RuleEngineV4.StudyAmendmentValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyArmValidator 'TransCelerate.SDR.RuleEngineV2.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyArmValidator 'TransCelerate.SDR.RuleEngineV3.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyArmValidator 'TransCelerate.SDR.RuleEngineV4.StudyArmValidator')
- [StudyCellValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyCellValidator 'TransCelerate.SDR.RuleEngineV4.StudyCellValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV2.StudyCellsValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV3.StudyCellsValidator')
- [StudyCohortValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyCohortValidator 'TransCelerate.SDR.RuleEngineV4.StudyCohortValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV2.StudyDefinitionsValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV3.StudyDefinitionsValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV4.StudyDefinitionsValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignPopulationValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV3.StudyDesignPopulationValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV4.StudyDesignPopulationValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV3.StudyDesignValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV4.StudyDesignValidator')
- [StudyElementValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyElementValidator 'TransCelerate.SDR.RuleEngineV4.StudyElementValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV2.StudyElementsValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV3.StudyElementsValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV2.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV3.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV4.StudyEpochValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV2.StudyEstimandsValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV3.StudyEstimandsValidator')
- [StudyIdentifierValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyIdentifierValidator 'TransCelerate.SDR.RuleEngineV4.StudyIdentifierValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV2.StudyIdentifiersValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV3.StudyIdentifiersValidator')
- [StudyInterventionValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyInterventionValidator 'TransCelerate.SDR.RuleEngineV4.StudyInterventionValidator')
- [StudyProtocolDocumentValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyProtocolDocumentValidator 'TransCelerate.SDR.RuleEngineV4.StudyProtocolDocumentValidator')
- [StudyProtocolDocumentVersionValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyProtocolDocumentVersionValidator 'TransCelerate.SDR.RuleEngineV4.StudyProtocolDocumentVersionValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV2.StudyProtocolVersionsValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV3.StudyProtocolVersionsValidator')
- [StudySiteValidator](#T-TransCelerate-SDR-RuleEngineV4-StudySiteValidator 'TransCelerate.SDR.RuleEngineV4.StudySiteValidator')
- [StudyTitleValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyTitleValidator 'TransCelerate.SDR.RuleEngineV4.StudyTitleValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyValidator 'TransCelerate.SDR.RuleEngineV2.StudyValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyValidator 'TransCelerate.SDR.RuleEngineV3.StudyValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyValidator 'TransCelerate.SDR.RuleEngineV4.StudyValidator')
- [StudyVersionValidator](#T-TransCelerate-SDR-RuleEngineV4-StudyVersionValidator 'TransCelerate.SDR.RuleEngineV4.StudyVersionValidator')
- [SubjectEnrollmentValidator](#T-TransCelerate-SDR-RuleEngineV4-SubjectEnrollmentValidator 'TransCelerate.SDR.RuleEngineV4.SubjectEnrollmentValidator')
- [SyntaxTemplateDictionaryValidator](#T-TransCelerate-SDR-RuleEngineV4-SyntaxTemplateDictionaryValidator 'TransCelerate.SDR.RuleEngineV4.SyntaxTemplateDictionaryValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV2-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV2.TransitionRuleValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV3-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV3.TransitionRuleValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV4-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV4.TransitionRuleValidator')
- [UserGroupsQueryParametersValidator](#T-TransCelerate-SDR-RuleEngine-UserGroupsQueryParametersValidator 'TransCelerate.SDR.RuleEngine.UserGroupsQueryParametersValidator')
- [UserLoginValidator](#T-TransCelerate-SDR-RuleEngine-UserLoginValidator 'TransCelerate.SDR.RuleEngine.UserLoginValidator')
- [ValidationDependenciesCommon](#T-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon 'TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon')
  - [AddValidationDependenciesCommon(services)](#M-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon-AddValidationDependenciesCommon-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon.AddValidationDependenciesCommon(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV2](#T-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2')
  - [AddValidationDependenciesV2(services)](#M-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2-AddValidationDependenciesV2-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2.AddValidationDependenciesV2(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV3](#T-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3 'TransCelerate.SDR.RuleEngineV3.ValidationDependenciesV3')
  - [AddValidationDependenciesV3(services)](#M-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3-AddValidationDependenciesV3-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV3.ValidationDependenciesV3.AddValidationDependenciesV3(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV4](#T-TransCelerate-SDR-RuleEngineV4-ValidationDependenciesV4 'TransCelerate.SDR.RuleEngineV4.ValidationDependenciesV4')
  - [AddValidationDependenciesV4(services)](#M-TransCelerate-SDR-RuleEngineV4-ValidationDependenciesV4-AddValidationDependenciesV4-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV4.ValidationDependenciesV4.AddValidationDependenciesV4(Microsoft.Extensions.DependencyInjection.IServiceCollection)')

<a name='T-TransCelerate-SDR-RuleEngineV2-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV2-AddressValidator'></a>
## AddressValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV3-AddressValidator'></a>
## AddressValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV4-AddressValidator'></a>
## AddressValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV4-AdministrationDurationValidator'></a>
## AdministrationDurationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV4-AgentAdministrationValidator'></a>
## AgentAdministrationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV2-AliasCodeValidator'></a>
## AliasCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for AliasCode

<a name='T-TransCelerate-SDR-RuleEngineV3-AliasCodeValidator'></a>
## AliasCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for AliasCode

<a name='T-TransCelerate-SDR-RuleEngineV4-AliasCodeValidator'></a>
## AliasCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for AliasCode

<a name='T-TransCelerate-SDR-RuleEngineV2-AnalysisPopulationValidator'></a>
## AnalysisPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Analysis Population

<a name='T-TransCelerate-SDR-RuleEngineV3-AnalysisPopulationValidator'></a>
## AnalysisPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Analysis Population

<a name='T-TransCelerate-SDR-RuleEngineV4-AnalysisPopulationValidator'></a>
## AnalysisPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Analysis Population

<a name='T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptCategoryValidator'></a>
## BiomedicalConceptCategoryValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptCategoryValidator'></a>
## BiomedicalConceptCategoryValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptCategoryValidator'></a>
## BiomedicalConceptCategoryValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptPropertyValidator'></a>
## BiomedicalConceptPropertyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptPropertyValidator'></a>
## BiomedicalConceptPropertyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptPropertyValidator'></a>
## BiomedicalConceptPropertyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptSurrogateValidator'></a>
## BiomedicalConceptSurrogateValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptSurrogateValidator'></a>
## BiomedicalConceptSurrogateValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptSurrogateValidator'></a>
## BiomedicalConceptSurrogateValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptValidator'></a>
## BiomedicalConceptValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptValidator'></a>
## BiomedicalConceptValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-BiomedicalConceptValidator'></a>
## BiomedicalConceptValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-CharacteristicValidator'></a>
## CharacteristicValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV2-CodeValidator'></a>
## CodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV3-CodeValidator'></a>
## CodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-CodeValidator'></a>
## CodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-ConditionAssignmentValidator'></a>
## ConditionAssignmentValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV4-ConditionValidator'></a>
## ConditionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV4-EligibilityCriterionValidator'></a>
## EligibilityCriterionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV2-EncounterValidator'></a>
## EncounterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Encounter

<a name='T-TransCelerate-SDR-RuleEngineV3-EncounterValidator'></a>
## EncounterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Encounter

<a name='T-TransCelerate-SDR-RuleEngineV4-EncounterValidator'></a>
## EncounterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Encounter

<a name='T-TransCelerate-SDR-RuleEngineV2-EndpointValidator'></a>
## EndpointValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV3-EndpointValidator'></a>
## EndpointValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV4-EndpointValidator'></a>
## EndpointValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV4-EstimandValidator'></a>
## EstimandValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyEstimands

<a name='T-TransCelerate-SDR-RuleEngineV4-GeographicScopeValidator'></a>
## GeographicScopeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-GovernanceDateValidator'></a>
## GovernanceDateValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngine-GroupFilterValidator'></a>
## GroupFilterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for Group Filter

<a name='T-TransCelerate-SDR-RuleEngine-GroupFilterValuesValidator'></a>
## GroupFilterValuesValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for Group Filter Values

<a name='T-TransCelerate-SDR-RuleEngine-GroupsValidator'></a>
## GroupsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for POST Group Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV2-IndicationValidator'></a>
## IndicationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Indication

<a name='T-TransCelerate-SDR-RuleEngineV3-IndicationValidator'></a>
## IndicationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Indication

<a name='T-TransCelerate-SDR-RuleEngineV4-IndicationValidator'></a>
## IndicationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Indication

<a name='T-TransCelerate-SDR-RuleEngineV2-InterCurrentEventsValidator'></a>
## InterCurrentEventsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for InterCurrent Events

<a name='T-TransCelerate-SDR-RuleEngineV3-InterCurrentEventsValidator'></a>
## InterCurrentEventsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for InterCurrent Events

<a name='T-TransCelerate-SDR-RuleEngineV4-IntercurrentEventValidator'></a>
## IntercurrentEventValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for InterCurrent Events

<a name='T-TransCelerate-SDR-RuleEngineV2-InvestigationalInterventionValidator'></a>
## InvestigationalInterventionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for InvestigationalIntervention

<a name='T-TransCelerate-SDR-RuleEngineV3-InvestigationalInterventionValidator'></a>
## InvestigationalInterventionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for InvestigationalIntervention

<a name='T-TransCelerate-SDR-RuleEngineV4-MaskingValidator'></a>
## MaskingValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-NarrativeContentValidator'></a>
## NarrativeContentValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for InterCurrent Events

<a name='T-TransCelerate-SDR-RuleEngineV2-ObjectiveValidator'></a>
## ObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV3-ObjectiveValidator'></a>
## ObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV4-ObjectiveValidator'></a>
## ObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV2-OrganisationValidator'></a>
## OrganisationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngineV3-OrganisationValidator'></a>
## OrganisationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngineV4-OrganizationValidator'></a>
## OrganizationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngineV4-ParameterMapValidator'></a>
## ParameterMapValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngine-PostUserToGroupValidator'></a>
## PostUserToGroupValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for POST User Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV2-ProcedureValidator'></a>
## ProcedureValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Procedure

<a name='T-TransCelerate-SDR-RuleEngineV3-ProcedureValidator'></a>
## ProcedureValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Procedure

<a name='T-TransCelerate-SDR-RuleEngineV4-ProcedureValidator'></a>
## ProcedureValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Procedure

<a name='T-TransCelerate-SDR-RuleEngineV4-QuantityValidator'></a>
## QuantityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-RangeValidator'></a>
## RangeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-ResearchOrganizationValidator'></a>
## ResearchOrganizationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngineV2-ResponseCodeValidator'></a>
## ResponseCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV3-ResponseCodeValidator'></a>
## ResponseCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-ResponseCodeValidator'></a>
## ResponseCodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyAmendmentReasonValidator'></a>
## StudyAmendmentReasonValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Activity

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyAmendmentValidator'></a>
## StudyAmendmentValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyArm

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyArm

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyArm

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyCellValidator'></a>
## StudyCellValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyCells

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyCellsValidator'></a>
## StudyCellsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyCells

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyCellsValidator'></a>
## StudyCellsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyCells

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyCohortValidator'></a>
## StudyCohortValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyDesignPopulation

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyDefinitionsValidator'></a>
## StudyDefinitionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyDefinitionsValidator'></a>
## StudyDefinitionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyDefinitionsValidator'></a>
## StudyDefinitionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyDesignPopulationValidator'></a>
## StudyDesignPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyDesignPopulation

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyDesignPopulationValidator'></a>
## StudyDesignPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyDesignPopulation

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyDesignPopulationValidator'></a>
## StudyDesignPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyDesignPopulation

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyDesignValidator'></a>
## StudyDesignValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyDesign

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyDesignValidator'></a>
## StudyDesignValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyDesign

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyDesignValidator'></a>
## StudyDesignValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyDesign

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyElementValidator'></a>
## StudyElementValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyElements

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyElementsValidator'></a>
## StudyElementsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyElements

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyElementsValidator'></a>
## StudyElementsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyElements

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyEpochValidator'></a>
## StudyEpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyEpoch

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyEpochValidator'></a>
## StudyEpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyEpoch

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyEpochValidator'></a>
## StudyEpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyEpoch

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyEstimandsValidator'></a>
## StudyEstimandsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyEstimands

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyEstimandsValidator'></a>
## StudyEstimandsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyEstimands

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyIdentifierValidator'></a>
## StudyIdentifierValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyIdentifiers

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyIdentifiersValidator'></a>
## StudyIdentifiersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyIdentifiers

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyIdentifiersValidator'></a>
## StudyIdentifiersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyIdentifiers

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyInterventionValidator'></a>
## StudyInterventionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for InvestigationalIntervention

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyProtocolDocumentValidator'></a>
## StudyProtocolDocumentValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyProtocolVersions

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyProtocolDocumentVersionValidator'></a>
## StudyProtocolDocumentVersionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for StudyProtocolVersions

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyProtocolVersionsValidator'></a>
## StudyProtocolVersionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyProtocolVersions

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyProtocolVersionsValidator'></a>
## StudyProtocolVersionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for StudyProtocolVersions

<a name='T-TransCelerate-SDR-RuleEngineV4-StudySiteValidator'></a>
## StudySiteValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyTitleValidator'></a>
## StudyTitleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Address

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyValidator'></a>
## StudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV3-StudyValidator'></a>
## StudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyValidator'></a>
## StudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-StudyVersionValidator'></a>
## StudyVersionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Study

<a name='T-TransCelerate-SDR-RuleEngineV4-SubjectEnrollmentValidator'></a>
## SubjectEnrollmentValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for Code

<a name='T-TransCelerate-SDR-RuleEngineV4-SyntaxTemplateDictionaryValidator'></a>
## SyntaxTemplateDictionaryValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for InvestigationalIntervention

<a name='T-TransCelerate-SDR-RuleEngineV2-TransitionRuleValidator'></a>
## TransitionRuleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for TransitionRule

<a name='T-TransCelerate-SDR-RuleEngineV3-TransitionRuleValidator'></a>
## TransitionRuleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

##### Summary

This Class is the validator for TransitionRule

<a name='T-TransCelerate-SDR-RuleEngineV4-TransitionRuleValidator'></a>
## TransitionRuleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

##### Summary

This Class is the validator for TransitionRule

<a name='T-TransCelerate-SDR-RuleEngine-UserGroupsQueryParametersValidator'></a>
## UserGroupsQueryParametersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for GET User and Groups Endpoints

<a name='T-TransCelerate-SDR-RuleEngine-UserLoginValidator'></a>
## UserLoginValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for Token Endpoint

<a name='T-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon'></a>
## ValidationDependenciesCommon `type`

##### Namespace

TransCelerate.SDR.RuleEngine.Common

<a name='M-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon-AddValidationDependenciesCommon-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependenciesCommon(services) `method`

##### Summary

Add all the dependencies for validations

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |

<a name='T-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2'></a>
## ValidationDependenciesV2 `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

<a name='M-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2-AddValidationDependenciesV2-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependenciesV2(services) `method`

##### Summary

Add all the dependencies for validations

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |

<a name='T-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3'></a>
## ValidationDependenciesV3 `type`

##### Namespace

TransCelerate.SDR.RuleEngineV3

<a name='M-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3-AddValidationDependenciesV3-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependenciesV3(services) `method`

##### Summary

Add all the dependencies for validations

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |

<a name='T-TransCelerate-SDR-RuleEngineV4-ValidationDependenciesV4'></a>
## ValidationDependenciesV4 `type`

##### Namespace

TransCelerate.SDR.RuleEngineV4

<a name='M-TransCelerate-SDR-RuleEngineV4-ValidationDependenciesV4-AddValidationDependenciesV4-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependenciesV4(services) `method`

##### Summary

Add all the dependencies for validations

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |
