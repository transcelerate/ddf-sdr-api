<a name='assembly'></a>
# TransCelerate.SDR.RuleEngine

## Contents

- [ActivityValidator](#T-TransCelerate-SDR-RuleEngine-ActivityValidator 'TransCelerate.SDR.RuleEngine.ActivityValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ActivityValidator-#ctor 'TransCelerate.SDR.RuleEngine.ActivityValidator.#ctor')
- [ClinicalStudyValidator](#T-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator 'TransCelerate.SDR.RuleEngine.ClinicalStudyValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator-#ctor 'TransCelerate.SDR.RuleEngine.ClinicalStudyValidator.#ctor')
- [CodingValidator](#T-TransCelerate-SDR-RuleEngine-CodingValidator 'TransCelerate.SDR.RuleEngine.CodingValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-CodingValidator-#ctor 'TransCelerate.SDR.RuleEngine.CodingValidator.#ctor')
- [DefinedProcedureValidator](#T-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator 'TransCelerate.SDR.RuleEngine.DefinedProcedureValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-DefinedProcedureValidator-#ctor 'TransCelerate.SDR.RuleEngine.DefinedProcedureValidator.#ctor')
- [EncounterValidator](#T-TransCelerate-SDR-RuleEngine-EncounterValidator 'TransCelerate.SDR.RuleEngine.EncounterValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EncounterValidator-#ctor 'TransCelerate.SDR.RuleEngine.EncounterValidator.#ctor')
- [EndpointsValidator](#T-TransCelerate-SDR-RuleEngine-EndpointsValidator 'TransCelerate.SDR.RuleEngine.EndpointsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EndpointsValidator-#ctor 'TransCelerate.SDR.RuleEngine.EndpointsValidator.#ctor')
- [EpochValidator](#T-TransCelerate-SDR-RuleEngine-EpochValidator 'TransCelerate.SDR.RuleEngine.EpochValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-EpochValidator-#ctor 'TransCelerate.SDR.RuleEngine.EpochValidator.#ctor')
- [InvestigationalInterventionValidatior](#T-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior 'TransCelerate.SDR.RuleEngine.InvestigationalInterventionValidatior')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-InvestigationalInterventionValidatior-#ctor 'TransCelerate.SDR.RuleEngine.InvestigationalInterventionValidatior.#ctor')
- [ItemValidator](#T-TransCelerate-SDR-RuleEngine-ItemValidator 'TransCelerate.SDR.RuleEngine.ItemValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-ItemValidator-#ctor 'TransCelerate.SDR.RuleEngine.ItemValidator.#ctor')
- [MatrixValidator](#T-TransCelerate-SDR-RuleEngine-MatrixValidator 'TransCelerate.SDR.RuleEngine.MatrixValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-MatrixValidator-#ctor 'TransCelerate.SDR.RuleEngine.MatrixValidator.#ctor')
- [PlannedWorkFlowValidator](#T-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator 'TransCelerate.SDR.RuleEngine.PlannedWorkFlowValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PlannedWorkFlowValidator-#ctor 'TransCelerate.SDR.RuleEngine.PlannedWorkFlowValidator.#ctor')
- [PointInTimeValidator](#T-TransCelerate-SDR-RuleEngine-PointInTimeValidator 'TransCelerate.SDR.RuleEngine.PointInTimeValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PointInTimeValidator-#ctor 'TransCelerate.SDR.RuleEngine.PointInTimeValidator.#ctor')
- [PostStudyValidator](#T-TransCelerate-SDR-RuleEngine-PostStudyValidator 'TransCelerate.SDR.RuleEngine.PostStudyValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-PostStudyValidator-#ctor 'TransCelerate.SDR.RuleEngine.PostStudyValidator.#ctor')
- [RuleValidator](#T-TransCelerate-SDR-RuleEngine-RuleValidator 'TransCelerate.SDR.RuleEngine.RuleValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-RuleValidator-#ctor 'TransCelerate.SDR.RuleEngine.RuleValidator.#ctor')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngine-SearchParametersValidator 'TransCelerate.SDR.RuleEngine.SearchParametersValidator')
- [SearchParametersValidator](#T-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator 'TransCelerate.SDR.RuleEngineV1.SearchParametersValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-SearchParametersValidator-#ctor 'TransCelerate.SDR.RuleEngine.SearchParametersValidator.#ctor')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator-#ctor 'TransCelerate.SDR.RuleEngineV1.SearchParametersValidator.#ctor')
- [StudyArmValidator](#T-TransCelerate-SDR-RuleEngine-StudyArmValidator 'TransCelerate.SDR.RuleEngine.StudyArmValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyArmValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyArmValidator.#ctor')
- [StudyCellsValidator](#T-TransCelerate-SDR-RuleEngine-StudyCellsValidator 'TransCelerate.SDR.RuleEngine.StudyCellsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyCellsValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyCellsValidator.#ctor')
- [StudyDataCollectionValidator](#T-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator 'TransCelerate.SDR.RuleEngine.StudyDataCollectionValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyDataCollectionValidator.#ctor')
- [StudyElementsValidator](#T-TransCelerate-SDR-RuleEngine-StudyElementsValidator 'TransCelerate.SDR.RuleEngine.StudyElementsValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyElementsValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyElementsValidator.#ctor')
- [StudyEpochValidator](#T-TransCelerate-SDR-RuleEngine-StudyEpochValidator 'TransCelerate.SDR.RuleEngine.StudyEpochValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyEpochValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyEpochValidator.#ctor')
- [StudyIdentifiersValidator](#T-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator 'TransCelerate.SDR.RuleEngine.StudyIdentifiersValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyIdentifiersValidator.#ctor')
- [StudyIndicationValidator](#T-TransCelerate-SDR-RuleEngine-StudyIndicationValidator 'TransCelerate.SDR.RuleEngine.StudyIndicationValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyIndicationValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyIndicationValidator.#ctor')
- [StudyObjectivesValidator](#T-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator 'TransCelerate.SDR.RuleEngine.StudyObjectivesValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyObjectivesValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyObjectivesValidator.#ctor')
- [StudyPopulationValidator](#T-TransCelerate-SDR-RuleEngine-StudyPopulationValidator 'TransCelerate.SDR.RuleEngine.StudyPopulationValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyPopulationValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyPopulationValidator.#ctor')
- [StudyProtocolValidator](#T-TransCelerate-SDR-RuleEngine-StudyProtocolValidator 'TransCelerate.SDR.RuleEngine.StudyProtocolValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-StudyProtocolValidator-#ctor 'TransCelerate.SDR.RuleEngine.StudyProtocolValidator.#ctor')
- [ValidationDependencies](#T-TransCelerate-SDR-RuleEngine-ValidationDependencies 'TransCelerate.SDR.RuleEngine.ValidationDependencies')
  - [AddValidationDependencies(services)](#M-TransCelerate-SDR-RuleEngine-ValidationDependencies-AddValidationDependencies-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngine.ValidationDependencies.AddValidationDependencies(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ValidationDependenciesV1](#T-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1')
  - [AddValidationDependenciesV1(services)](#M-TransCelerate-SDR-RuleEngineV1-ValidationDependenciesV1-AddValidationDependenciesV1-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.RuleEngineV1.ValidationDependenciesV1.AddValidationDependenciesV1(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [WorkFlowItemMatrixValidator](#T-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator 'TransCelerate.SDR.RuleEngine.WorkFlowItemMatrixValidator')
  - [#ctor()](#M-TransCelerate-SDR-RuleEngine-WorkFlowItemMatrixValidator-#ctor 'TransCelerate.SDR.RuleEngine.WorkFlowItemMatrixValidator.#ctor')

<a name='T-TransCelerate-SDR-RuleEngine-ActivityValidator'></a>
## ActivityValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-ActivityValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Activity

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator'></a>
## ClinicalStudyValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

<a name='M-TransCelerate-SDR-RuleEngine-ClinicalStudyValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for ClinicalStudy

##### Parameters

This constructor has no parameters.

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

<a name='M-TransCelerate-SDR-RuleEngine-EncounterValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for Encounter

##### Parameters

This constructor has no parameters.

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

<a name='M-TransCelerate-SDR-RuleEngine-SearchParametersValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for SearchParameters

##### Parameters

This constructor has no parameters.

<a name='M-TransCelerate-SDR-RuleEngineV1-SearchParametersValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for SearchParameters

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyArmValidator'></a>
## StudyArmValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyDataCollectionValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyDataCollection

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyElementsValidator'></a>
## StudyElementsValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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

<a name='M-TransCelerate-SDR-RuleEngine-StudyEpochValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Validator for StudyEpoch

##### Parameters

This constructor has no parameters.

<a name='T-TransCelerate-SDR-RuleEngine-StudyIdentifiersValidator'></a>
## StudyIdentifiersValidator `type`

##### Namespace

TransCelerate.SDR.RuleEngine

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
