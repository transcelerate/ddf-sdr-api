<a name='assembly'></a>
# TransCelerate.SDR.RuleEngine

## Contents

- [ActivityValidator](#T-TransCelerate-SDR-RuleEngine-ActivityValidator 'TransCelerate.SDR.RuleEngine.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV1-ActivityValidator 'TransCelerate.SDR.RuleEngineV1.ActivityValidator')
- [ActivityValidator](#T-TransCelerate-SDR-RuleEngineV2-ActivityValidator 'TransCelerate.SDR.RuleEngineV2.ActivityValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ActivityValidator-#ctor 'TransCelerate.SDR.RuleEngine.ActivityValidator.#ctor')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV1-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV1.AnalysisPopulationValidator')
- [AnalysisPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-AnalysisPopulationValidator 'TransCelerate.SDR.RuleEngineV2.AnalysisPopulationValidator')
- [ClinicalStudyValidator](#T-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator 'TransCelerate.SDR.RuleEngine.ClinicalStudyValidator')
- [ClinicalStudyValidator](#T-TransCelerate-SDR-RuleEngineV1-ClinicalStudyValidator 'TransCelerate.SDR.RuleEngineV1.ClinicalStudyValidator')
- [ClinicalStudyValidator](#T-TransCelerate-SDR-RuleEngineV2-ClinicalStudyValidator 'TransCelerate.SDR.RuleEngineV2.ClinicalStudyValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator-#ctor 'TransCelerate.SDR.RuleEngine.ClinicalStudyValidator.#ctor')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV1-CodeValidator 'TransCelerate.SDR.RuleEngineV1.CodeValidator')
- [CodeValidator](#T-TransCelerate-SDR-RuleEngineV2-CodeValidator 'TransCelerate.SDR.RuleEngineV2.CodeValidator')
- [CodingValidator](#T-TransCelerate-SDR-RuleEngine-CodingValidator 'TransCelerate.SDR.RuleEngine.CodingValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-CodingValidator-#ctor 'TransCelerate.SDR.RuleEngine.CodingValidator.#ctor')
- [DefinedProcedureValidator](#T-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator 'TransCelerate.SDR.RuleEngine.DefinedProcedureValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator-#ctor 'TransCelerate.SDR.RuleEngine.DefinedProcedureValidator.#ctor')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngine-EncounterValidator 'TransCelerate.SDR.RuleEngine.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV1-EncounterValidator 'TransCelerate.SDR.RuleEngineV1.EncounterValidator')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngineV2-EncounterValidator 'TransCelerate.SDR.RuleEngineV2.EncounterValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EncounterValidator-#ctor 'TransCelerate.SDR.RuleEngine.EncounterValidator.#ctor')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV1-EndpointValidator 'TransCelerate.SDR.RuleEngineV1.EndpointValidator')
- [EndpointValidator](#T-TransCelerate-SDR-RuleEngineV2-EndpointValidator 'TransCelerate.SDR.RuleEngineV2.EndpointValidator')
- [EndpointsValidator](#T-TransCelerate-SDR-RuleEngine-EndpointsValidator 'TransCelerate.SDR.RuleEngine.EndpointsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EndpointsValidator-#ctor 'TransCelerate.SDR.RuleEngine.EndpointsValidator.#ctor')
- [EpochValidator](#T-TransCelerate-SDR-RuleEngine-EpochValidator 'TransCelerate.SDR.RuleEngine.EpochValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EpochValidator-#ctor 'TransCelerate.SDR.RuleEngine.EpochValidator.#ctor')
- [GroupFilterValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValidator')
- [GroupFilterValuesValidator](#T-TransCelerate-SDR-RuleEngine-GroupFilterValuesValidator 'TransCelerate.SDR.RuleEngine.GroupFilterValuesValidator')
- [GroupsValidator](#T-TransCelerate-SDR-RuleEngine-GroupsValidator 'TransCelerate.SDR.RuleEngine.GroupsValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV1-IndicationValidator 'TransCelerate.SDR.RuleEngineV1.IndicationValidator')
- [IndicationValidator](#T-TransCelerate-SDR-RuleEngineV2-IndicationValidator 'TransCelerate.SDR.RuleEngineV2.IndicationValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV1-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV1.InterCurrentEventsValidator')
- [InterCurrentEventsValidator](#T-TransCelerate-SDR-RuleEngineV2-InterCurrentEventsValidator 'TransCelerate.SDR.RuleEngineV2.InterCurrentEventsValidator')
- [InvestigationalInterventionValidatior](#T-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior 'TransCelerate.SDR.RuleEngine.InvestigationalInterventionValidatior')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior-#ctor 'TransCelerate.SDR.RuleEngine.InvestigationalInterventionValidatior.#ctor')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV1-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV1.InvestigationalInterventionValidator')
- [InvestigationalInterventionValidator](#T-TransCelerate-SDR-RuleEngineV2-InvestigationalInterventionValidator 'TransCelerate.SDR.RuleEngineV2.InvestigationalInterventionValidator')
- [ItemValidator](#T-TransCelerate-SDR-RuleEngine-ItemValidator 'TransCelerate.SDR.RuleEngine.ItemValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ItemValidator-#ctor 'TransCelerate.SDR.RuleEngine.ItemValidator.#ctor')
- [MatrixValidator](#T-TransCelerate-SDR-RuleEngine-MatrixValidator 'TransCelerate.SDR.RuleEngine.MatrixValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-MatrixValidator-#ctor 'TransCelerate.SDR.RuleEngine.MatrixValidator.#ctor')
- [OrganisationValidator](#T-TransCelerate-SDR-RuleEngineV2-OrganisationValidator 'TransCelerate.SDR.RuleEngineV2.OrganisationValidator')
- [PlannedWorkFlowValidator](#T-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator 'TransCelerate.SDR.RuleEngine.PlannedWorkFlowValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator-#ctor 'TransCelerate.SDR.RuleEngine.PlannedWorkFlowValidator.#ctor')
- [PointInTimeValidator](#T-TransCelerate-SDR-RuleEngine-PointInTimeValidator 'TransCelerate.SDR.RuleEngine.PointInTimeValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PointInTimeValidator-#ctor 'TransCelerate.SDR.RuleEngine.PointInTimeValidator.#ctor')
- [PostStudyValidator](#T-TransCelerate-SDR-RuleEngine-PostStudyValidator 'TransCelerate.SDR.RuleEngine.PostStudyValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PostStudyValidator-#ctor 'TransCelerate.SDR.RuleEngine.PostStudyValidator.#ctor')
- [PostUserToGroupValidator](#T-TransCelerate-SDR-RuleEngine-PostUserToGroupValidator 'TransCelerate.SDR.RuleEngine.PostUserToGroupValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV1-ProcedureValidator 'TransCelerate.SDR.RuleEngineV1.ProcedureValidator')
- [ProcedureValidator](#T-TransCelerate-SDR-RuleEngineV2-ProcedureValidator 'TransCelerate.SDR.RuleEngineV2.ProcedureValidator')
- [RuleValidator](#T-TransCelerate-SDR-RuleEngine-RuleValidator 'TransCelerate.SDR.RuleEngine.RuleValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-RuleValidator-#ctor 'TransCelerate.SDR.RuleEngine.RuleValidator.#ctor')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngine-SearchParametersValidator 'TransCelerate.SDR.RuleEngine.SearchParametersValidator')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator 'TransCelerate.SDR.RuleEngineV1.SearchParametersValidator')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngineV2-SearchParametersValidator 'TransCelerate.SDR.RuleEngineV2.SearchParametersValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-SearchParametersValidator-#ctor 'TransCelerate.SDR.RuleEngine.SearchParametersValidator.#ctor')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngine-StudyArmValidator 'TransCelerate.SDR.RuleEngine.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyArmValidator 'TransCelerate.SDR.RuleEngineV1.StudyArmValidator')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyArmValidator 'TransCelerate.SDR.RuleEngineV2.StudyArmValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyArmValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyArmValidator.#ctor')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngine-StudyCellsValidator 'TransCelerate.SDR.RuleEngine.StudyCellsValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV1.StudyCellsValidator')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyCellsValidator 'TransCelerate.SDR.RuleEngineV2.StudyCellsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyCellsValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyCellsValidator.#ctor')
- [StudyDataCollectionValidator](#T-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator 'TransCelerate.SDR.RuleEngine.StudyDataCollectionValidator')
- [StudyDataCollectionValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDataCollectionValidator 'TransCelerate.SDR.RuleEngineV1.StudyDataCollectionValidator')
- [StudyDataCollectionValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDataCollectionValidator 'TransCelerate.SDR.RuleEngineV2.StudyDataCollectionValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyDataCollectionValidator.#ctor')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV1.StudyDesignPopulationValidator')
- [StudyDesignPopulationValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignPopulationValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignPopulationValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV1.StudyDesignValidator')
- [StudyDesignValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyDesignValidator 'TransCelerate.SDR.RuleEngineV2.StudyDesignValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngine-StudyElementsValidator 'TransCelerate.SDR.RuleEngine.StudyElementsValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV1.StudyElementsValidator')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyElementsValidator 'TransCelerate.SDR.RuleEngineV2.StudyElementsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyElementsValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyElementsValidator.#ctor')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngine-StudyEpochValidator 'TransCelerate.SDR.RuleEngine.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV1.StudyEpochValidator')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEpochValidator 'TransCelerate.SDR.RuleEngineV2.StudyEpochValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyEpochValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyEpochValidator.#ctor')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV1.StudyEstimandsValidator')
- [StudyEstimandsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyEstimandsValidator 'TransCelerate.SDR.RuleEngineV2.StudyEstimandsValidator')
- [StudyIdentifierScopeValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyIdentifierScopeValidator 'TransCelerate.SDR.RuleEngineV1.StudyIdentifierScopeValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngine.StudyIdentifiersValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV1.StudyIdentifiersValidator')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngineV2.StudyIdentifiersValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyIdentifiersValidator.#ctor')
- [StudyIndicationValidator](#T-TransCelerate-SDR-RuleEngine-StudyIndicationValidator 'TransCelerate.SDR.RuleEngine.StudyIndicationValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyIndicationValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyIndicationValidator.#ctor')
- [StudyObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyObjectiveValidator 'TransCelerate.SDR.RuleEngineV1.StudyObjectiveValidator')
- [StudyObjectiveValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyObjectiveValidator 'TransCelerate.SDR.RuleEngineV2.StudyObjectiveValidator')
- [StudyObjectivesValidator](#T-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator 'TransCelerate.SDR.RuleEngine.StudyObjectivesValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyObjectivesValidator.#ctor')
- [StudyPopulationValidator](#T-TransCelerate-SDR-RuleEngine-StudyPopulationValidator 'TransCelerate.SDR.RuleEngine.StudyPopulationValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyPopulationValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyPopulationValidator.#ctor')
- [StudyProtocolValidator](#T-TransCelerate-SDR-RuleEngine-StudyProtocolValidator 'TransCelerate.SDR.RuleEngine.StudyProtocolValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyProtocolValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyProtocolValidator.#ctor')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV1.StudyProtocolVersionsValidator')
- [StudyProtocolVersionsValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyProtocolVersionsValidator 'TransCelerate.SDR.RuleEngineV2.StudyProtocolVersionsValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV1-StudyValidator 'TransCelerate.SDR.RuleEngineV1.StudyValidator')
- [StudyValidator](#T-TransCelerate-SDR-RuleEngineV2-StudyValidator 'TransCelerate.SDR.RuleEngineV2.StudyValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV1-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV1.TransitionRuleValidator')
- [TransitionRuleValidator](#T-TransCelerate-SDR-RuleEngineV2-TransitionRuleValidator 'TransCelerate.SDR.RuleEngineV2.TransitionRuleValidator')
- [UserGroupsQueryParametersValidator](#T-TransCelerate-SDR-RuleEngine-UserGroupsQueryParametersValidator 'TransCelerate.SDR.RuleEngine.UserGroupsQueryParametersValidator')
- [UserLoginValidator](#T-TransCelerate-SDR-RuleEngine-UserLoginValidator 'TransCelerate.SDR.RuleEngine.UserLoginValidator')
- [ValidationDependencies](#T-TransCelerate-SDR-RuleEngine-ValidationDependencies 'TransCelerate.SDR.RuleEngine.ValidationDependencies')
  - [AddValidationDependencies(services)](#M-TransCelerate-SDR-RuleEngine-ValidationDependencies-AddValidationDependencies-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngine.ValidationDependencies.AddValidationDependencies(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV1](#T-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1')
  - [AddValidationDependenciesV1(services)](#M-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1-AddValidationDependenciesV1-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1.AddValidationDependenciesV1(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV2](#T-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2')
  - [AddValidationDependenciesV2(services)](#M-TransCelerate-SDR-RuleEngineV2-ValidationDependenciesV2-AddValidationDependenciesV2-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV2.ValidationDependenciesV2.AddValidationDependenciesV2(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [WorkFlowItemMatrixValidator](#T-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator 'TransCelerate.SDR.RuleEngine.WorkFlowItemMatrixValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator-#ctor 'TransCelerate.SDR.RuleEngine.WorkFlowItemMatrixValidator.#ctor')
- [WorkflowItemValidator](#T-TransCelerate-SDR-RuleEngineV1-WorkflowItemValidator 'TransCelerate.SDR.RuleEngineV1.WorkflowItemValidator')
- [WorkflowItemValidator](#T-TransCelerate-SDR-RuleEngineV2-WorkflowItemValidator 'TransCelerate.SDR.RuleEngineV2.WorkflowItemValidator')
- [WorkflowValidator](#T-TransCelerate-SDR-RuleEngineV1-WorkflowValidator 'TransCelerate.SDR.RuleEngineV1.WorkflowValidator')
- [WorkflowValidator](#T-TransCelerate-SDR-RuleEngineV2-WorkflowValidator 'TransCelerate.SDR.RuleEngineV2.WorkflowValidator')

<a name='T-TransCelerate-SDR-RuleEngine-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-ActivityValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Activity

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator'></a>
## ClinicalStudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='T-TransCelerate-SDR-RuleEngineV1-ClinicalStudyValidator'></a>
## ClinicalStudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for ClinicalStudy

<a name='T-TransCelerate-SDR-RuleEngineV2-ClinicalStudyValidator'></a>
## ClinicalStudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for ClinicalStudy

<a name='M-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for ClinicalStudy

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-CodingValidator'></a>
## CodingValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-CodingValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Coding

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator'></a>
## DefinedProcedureValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for DefinedProcedure

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-EncounterValidator'></a>
## EncounterValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-EncounterValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Encounter

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-EndpointsValidator'></a>
## EndpointsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-EndpointsValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Endpoints

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-EpochValidator'></a>
## EpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-EpochValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Epoch

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior'></a>
## InvestigationalInterventionValidatior `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for InvestigationalIntervention

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-ItemValidator'></a>
## ItemValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-ItemValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Items

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-MatrixValidator'></a>
## MatrixValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-MatrixValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Matrix

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngineV2-OrganisationValidator'></a>
## OrganisationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator'></a>
## PlannedWorkFlowValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Planned Workflow

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-PointInTimeValidator'></a>
## PointInTimeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-PointInTimeValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for PointInTime

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-PostStudyValidator'></a>
## PostStudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-PostStudyValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for ClinicalStudy

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-RuleValidator'></a>
## RuleValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-RuleValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Rule

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-SearchParametersValidator'></a>
## SearchParametersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='T-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator'></a>
## SearchParametersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for SearchParameters

<a name='T-TransCelerate-SDR-RuleEngineV2-SearchParametersValidator'></a>
## SearchParametersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for SearchParameters

<a name='M-TransCelerate-SDR-RuleEngine-SearchParametersValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for SearchParameters

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyArmValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyArm

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyCellsValidator'></a>
## StudyCellsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyCellsValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyCells

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator'></a>
## StudyDataCollectionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyDataCollectionValidator'></a>
## StudyDataCollectionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyDataCollection

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyDataCollectionValidator'></a>
## StudyDataCollectionValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyDataCollection

<a name='M-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyDataCollection

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-StudyElementsValidator'></a>
## StudyElementsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyElementsValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyElemnts

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyEpochValidator'></a>
## StudyEpochValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyEpochValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyEpoch

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyIdentifierScopeValidator'></a>
## StudyIdentifierScopeValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyIdentifierScope

<a name='T-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator'></a>
## StudyIdentifiersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyIdentifiers

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyIndicationValidator'></a>
## StudyIndicationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-StudyIndicationValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyIndication

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngineV1-StudyObjectiveValidator'></a>
## StudyObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngineV2-StudyObjectiveValidator'></a>
## StudyObjectiveValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for StudyObjectives

<a name='T-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator'></a>
## StudyObjectivesValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyObjectives

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyPopulationValidator'></a>
## StudyPopulationValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-StudyPopulationValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyPopulations

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyProtocolValidator'></a>
## StudyProtocolValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-StudyProtocolValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for studyProtocol

##### Parameters

This constructor has no parameters.

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

<a name='T-TransCelerate-SDR-RuleEngine-ValidationDependencies'></a>
## ValidationDependencies `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-ValidationDependencies-AddValidationDependencies-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddValidationDependencies(services) `method`

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

<a name='T-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator'></a>
## WorkFlowItemMatrixValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for WorkFlowItemMatrix

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngineV1-WorkflowItemValidator'></a>
## WorkflowItemValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for WorkFlowItems

<a name='T-TransCelerate-SDR-RuleEngineV2-WorkflowItemValidator'></a>
## WorkflowItemValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for WorkFlowItems

<a name='T-TransCelerate-SDR-RuleEngineV1-WorkflowValidator'></a>
## WorkflowValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV1

##### Summary

This Class is the validator for WorkFlows

<a name='T-TransCelerate-SDR-RuleEngineV2-WorkflowValidator'></a>
## WorkflowValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngineV2

##### Summary

This Class is the validator for WorkFlows
