<a name='assembly'></a>
# TransCelerate.SDR.RuleEngine

## Contents

- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV1-ActivityValidator 'TransCelerate.SDR.RuleEngineV1.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV2-ActivityValidator 'TransCelerate.SDR.RuleEngineV2.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV3-ActivityValidator 'TransCelerate.SDR.RuleEngineV3.ActivityValidator')
- [AddressValidator](#T-TransCelerate-SDR-RuleEngineV2-AddressValidator 'TransCelerate.SDR.RuleEngineV2.AddressValidator')
- [AddressValidator](#T-TransCelerate-SDR-RuleEngineV3-AddressValidator 'TransCelerate.SDR.RuleEngineV3.AddressValidator')
- [AliasCodeValidator](#T-TransCelerate-SDR-RuleEngineV2-AliasCodeValidator 'TransCelerate.SDR.RuleEngineV2.AliasCodeValidator')
- [AliasCodeValidator](#T-TransCelerate-SDR-RuleEngineV3-AliasCodeValidator 'TransCelerate.SDR.RuleEngineV3.AliasCodeValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV1-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV1.AnalysisPopulationValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV2.AnalysisPopulationValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV3-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV3.AnalysisPopulationValidator')
- [BiomedicalConceptCategoryValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptCategoryValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptCategoryValidator')
- [BiomedicalConceptCategoryValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptCategoryValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptCategoryValidator')
- [BiomedicalConceptPropertyValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptPropertyValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptPropertyValidator')
- [BiomedicalConceptPropertyValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptPropertyValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptPropertyValidator')
- [BiomedicalConceptSurrogateValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptSurrogateValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptSurrogateValidator')
- [BiomedicalConceptSurrogateValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptSurrogateValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptSurrogateValidator')
- [BiomedicalConceptValidator](#T-TransCelerate-SDR-RuleEngineV2-BiomedicalConceptValidator 'TransCelerate.SDR.RuleEngineV2.BiomedicalConceptValidator')
- [BiomedicalConceptValidator](#T-TransCelerate-SDR-RuleEngineV3-BiomedicalConceptValidator 'TransCelerate.SDR.RuleEngineV3.BiomedicalConceptValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV1-CodeValidator 'TransCelerate.SDR.RuleEngineV1.CodeValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV2-CodeValidator 'TransCelerate.SDR.RuleEngineV2.CodeValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV3-CodeValidator 'TransCelerate.SDR.RuleEngineV3.CodeValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV1-EncounterValidator 'TransCelerate.SDR.RuleEngineV1.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV2-EncounterValidator 'TransCelerate.SDR.RuleEngineV2.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV3-EncounterValidator 'TransCelerate.SDR.RuleEngineV3.EncounterValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV1-EndpointValidator 'TransCelerate.SDR.RuleEngineV1.EndpointValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV2-EndpointValidator 'TransCelerate.SDR.RuleEngineV2.EndpointValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV3-EndpointValidator 'TransCelerate.SDR.RuleEngineV3.EndpointValidator')
- [GroupFilterValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValidator')
- [GroupFilterValuesValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValuesValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValuesValidator')
- [GroupsValidator](#T-TransCelerate-SDR-RuleEngine-GroupsValidator 'TransCelerate.SDR.RuleEngine.GroupsValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV1-IndicationValidator 'TransCelerate.SDR.RuleEngineV1.IndicationValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV2-IndicationValidator 'TransCelerate.SDR.RuleEngineV2.IndicationValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV3-IndicationValidator 'TransCelerate.SDR.RuleEngineV3.IndicationValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV1-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV1.InterCurrentEventsValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV2-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV2.InterCurrentEventsValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV3-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV3.InterCurrentEventsValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV1-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV1.InvestigationalInterventionValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV2-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV2.InvestigationalInterventionValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV3-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV3.InvestigationalInterventionValidator')
- [ObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV2-ObjectiveValidator 'TransCelerate.SDR.RuleEngineV2.ObjectiveValidator')
- [ObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV3-ObjectiveValidator 'TransCelerate.SDR.RuleEngineV3.ObjectiveValidator')
- [OrganisationValidator](#T-TransCelerate-SDR-RuleEngineV2-OrganisationValidator 'TransCelerate.SDR.RuleEngineV2.OrganisationValidator')
- [OrganisationValidator](#T-TransCelerate-SDR-RuleEngineV3-OrganisationValidator 'TransCelerate.SDR.RuleEngineV3.OrganisationValidator')
- [PostUserToGroupValidator](#T-TransCelerate-SDR-RuleEngine-PostUserToGroupValidator 'TransCelerate.SDR.RuleEngine.PostUserToGroupValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV1-ProcedureValidator 'TransCelerate.SDR.RuleEngineV1.ProcedureValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV2-ProcedureValidator 'TransCelerate.SDR.RuleEngineV2.ProcedureValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV3-ProcedureValidator 'TransCelerate.SDR.RuleEngineV3.ProcedureValidator')
- [ResponseCodeValidator](#T-TransCelerate-SDR-RuleEngineV2-ResponseCodeValidator 'TransCelerate.SDR.RuleEngineV2.ResponseCodeValidator')
- [ResponseCodeValidator](#T-TransCelerate-SDR-RuleEngineV3-ResponseCodeValidator 'TransCelerate.SDR.RuleEngineV3.ResponseCodeValidator')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator 'TransCelerate.SDR.RuleEngineV1.SearchParametersValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyArmValidator 'TransCelerate.SDR.RuleEngineV1.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyArmValidator 'TransCelerate.SDR.RuleEngineV2.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyArmValidator 'TransCelerate.SDR.RuleEngineV3.StudyArmValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV1.StudyCellsValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV2.StudyCellsValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV3.StudyCellsValidator')
- [StudyDataCollectionValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDataCollectionValidator 'TransCelerate.SDR.RuleEngineV1.StudyDataCollectionValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV1.StudyDefinitionsValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV2.StudyDefinitionsValidator')
- [StudyDefinitionsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDefinitionsValidator 'TransCelerate.SDR.RuleEngineV3.StudyDefinitionsValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV1.StudyDesignPopulationValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignPopulationValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV3.StudyDesignPopulationValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV1.StudyDesignValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV3.StudyDesignValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV1.StudyElementsValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV2.StudyElementsValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV3.StudyElementsValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV1.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV2.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV3.StudyEpochValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV1.StudyEstimandsValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV2.StudyEstimandsValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV3.StudyEstimandsValidator')
- [StudyIdentifierScopeValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyIdentifierScopeValidator 'TransCelerate.SDR.RuleEngineV1.StudyIdentifierScopeValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV1.StudyIdentifiersValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV2.StudyIdentifiersValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV3.StudyIdentifiersValidator')
- [StudyObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyObjectiveValidator 'TransCelerate.SDR.RuleEngineV1.StudyObjectiveValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV1.StudyProtocolVersionsValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV2.StudyProtocolVersionsValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV3.StudyProtocolVersionsValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyValidator 'TransCelerate.SDR.RuleEngineV1.StudyValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyValidator 'TransCelerate.SDR.RuleEngineV2.StudyValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV3-StudyValidator 'TransCelerate.SDR.RuleEngineV3.StudyValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV1-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV1.TransitionRuleValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV2-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV2.TransitionRuleValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV3-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV3.TransitionRuleValidator')
- [UserGroupsQueryParametersValidator](#T-TransCelerate-SDR-RuleEngine-UserGroupsQueryParametersValidator 'TransCelerate.SDR.RuleEngine.UserGroupsQueryParametersValidator')
- [UserLoginValidator](#T-TransCelerate-SDR-RuleEngine-UserLoginValidator 'TransCelerate.SDR.RuleEngine.UserLoginValidator')
- [ValidationDependenciesCommon](#T-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon 'TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon')
  - [AddValidationDependenciesCommon(services)](#M-TransCelerate-SDR-RuleEngine-Common-ValidationDependenciesCommon-AddValidationDependenciesCommon-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon.AddValidationDependenciesCommon(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV1](#T-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1')
  - [AddValidationDependenciesV1(services)](#M-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1-AddValidationDependenciesV1-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1.AddValidationDependenciesV1(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV2](#T-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2')
  - [AddValidationDependenciesV2(services)](#M-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2-AddValidationDependenciesV2-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2.AddValidationDependenciesV2(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV3](#T-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3 'TransCelerate.SDR.RuleEngineV3.ValidationDependenciesV3')
  - [AddValidationDependenciesV3(services)](#M-TransCelerate-SDR-RuleEngineV3-ValidationDependenciesV3-AddValidationDependenciesV3-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV3.ValidationDependenciesV3.AddValidationDependenciesV3(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [WorkflowItemValidator](#T-TransCelerate-SDR-RuleEngineV1-WorkflowItemValidator 'TransCelerate.SDR.RuleEngineV1.WorkflowItemValidator')
- [WorkflowValidator](#T-TransCelerate-SDR-RuleEngineV1-WorkflowValidator 'TransCelerate.SDR.RuleEngineV1.WorkflowValidator')

<a name='T-TransCelerate-SDR-RuleEngineV1-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Activity

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

<a name='T-TransCelerate-SDR-RuleEngineV1-AnalysisPopulationValidator'></a>
## AnalysisPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Analysis Population

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

<a name='T-TransCelerate-SDR-RuleEngineV1-CodeValidator'></a>
## CodeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

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

<a name='T-TransCelerate-SDR-RuleEngineV1-EncounterValidator'></a>
## EncounterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Encounter

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

<a name='T-TransCelerate-SDR-RuleEngineV1-EndpointValidator'></a>
## EndpointValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Endpoint

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

<a name='T-TransCelerate-SDR-RuleEngineV1-IndicationValidator'></a>
## IndicationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Indication

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

<a name='T-TransCelerate-SDR-RuleEngineV1-InterCurrentEventsValidator'></a>
## InterCurrentEventsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for InterCurrent Events

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

<a name='T-TransCelerate-SDR-RuleEngineV1-InvestigationalInterventionValidator'></a>
## InvestigationalInterventionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for InvestigationalIntervention

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

<a name='T-TransCelerate-SDR-RuleEngine-PostUserToGroupValidator'></a>
## PostUserToGroupValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

##### Summary

This Class is the validator for POST User Endpoint

<a name='T-TransCelerate-SDR-RuleEngineV1-ProcedureValidator'></a>
## ProcedureValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Procedure

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

<a name='T-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator'></a>
## SearchParametersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for SearchParameters

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyArm

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyCellsValidator'></a>
## StudyCellsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyDataCollectionValidator'></a>
## StudyDataCollectionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyDataCollection

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyDefinitionsValidator'></a>
## StudyDefinitionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Study

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyDesignPopulationValidator'></a>
## StudyDesignPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyDesignPopulation

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyDesignValidator'></a>
## StudyDesignValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyDesign

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyElementsValidator'></a>
## StudyElementsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyEpochValidator'></a>
## StudyEpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyEpoch

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyEstimandsValidator'></a>
## StudyEstimandsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyEstimands

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyIdentifierScopeValidator'></a>
## StudyIdentifierScopeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyIdentifiersValidator'></a>
## StudyIdentifiersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyObjectiveValidator'></a>
## StudyObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyProtocolVersionsValidator'></a>
## StudyProtocolVersionsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyValidator'></a>
## StudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for Study

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

<a name='T-TransCelerate-SDR-RuleEngineV1-TransitionRuleValidator'></a>
## TransitionRuleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for TransitionRule

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

<a name='T-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1'></a>
## ValidationDependenciesV1 `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

<a name='M-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1-AddValidationDependenciesV1-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependenciesV1(services) `method`

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

<a name='T-TransCelerate-SDR-RuleEngineV1-WorkflowItemValidator'></a>
## WorkflowItemValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for WorkFlowItems

<a name='T-TransCelerate-SDR-RuleEngineV1-WorkflowValidator'></a>
## WorkflowValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for WorkFlows
