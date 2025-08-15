<a name='assembly'></a>
# TransCelerate.SDR.Core

## Contents

- [ActionFilter](#T-TransCelerate-SDR-Core-Filters-ActionFilter 'TransCelerate.SDR.Core.Filters.ActionFilter')
- [ApiBehaviourOptionsHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper 'TransCelerate.SDR.Core.Utilities.Helpers.ApiBehaviourOptionsHelper')
  - [ModelStateResponse(context)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper-ModelStateResponse-Microsoft-AspNetCore-Mvc-ActionContext- 'TransCelerate.SDR.Core.Utilities.Helpers.ApiBehaviourOptionsHelper.ModelStateResponse(Microsoft.AspNetCore.Mvc.ActionContext)')
- [Config](#T-TransCelerate-SDR-Core-Utilities-Common-Config 'TransCelerate.SDR.Core.Utilities.Common.Config')
- [Constants](#T-TransCelerate-SDR-Core-Utilities-Common-Constants 'TransCelerate.SDR.Core.Utilities.Common.Constants')
- [DateValidationHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper 'TransCelerate.SDR.Core.Utilities.Helpers.DateValidationHelper')
  - [IsValid(value)](#M-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper-IsValid-System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.DateValidationHelper.IsValid(System.Object)')
- [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel')
- [ErrorResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper')
  - [BadRequest(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.BadRequest(System.String)')
  - [BadRequest(validationProblemDetails,message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-Object,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.BadRequest(System.Object,System.String)')
  - [ErrorResponseModel(exception)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-ErrorResponseModel-System-Exception- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.ErrorResponseModel(System.Exception)')
  - [GatewayError()](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-GatewayError 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.GatewayError')
  - [InternalServerError(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-InternalServerError-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.InternalServerError(System.String)')
  - [MethodNotAllowed(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-MethodNotAllowed-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.MethodNotAllowed(System.String)')
  - [NotFound(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-NotFound-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.NotFound(System.String)')
  - [ValidationBadRequest(validationErrorDetails,validationWarningDetails,message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-ValidationBadRequest-System-Object,System-Object,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.ValidationBadRequest(System.Object,System.Object,System.String)')
- [FromDateToDateHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper 'TransCelerate.SDR.Core.Utilities.Helpers.FromDateToDateHelper')
  - [GetFromAndToDate(fromDate,toDate,range)](#M-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper-GetFromAndToDate-System-DateTime,System-DateTime,System-Int32- 'TransCelerate.SDR.Core.Utilities.Helpers.FromDateToDateHelper.GetFromAndToDate(System.DateTime,System.DateTime,System.Int32)')
- [HelperV2](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2')
  - [AreValidStudyDesignElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.GetAuditTrail(System.String)')
  - [GetChangedValues(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.GetChangedValues(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForActivities(activities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForActivities-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ActivityEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForActivities(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ActivityEntity})')
  - [RemoveIdForAliasCode(aliasCode)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForAliasCode-TransCelerate-SDR-Core-Entities-StudyV2-AliasCodeEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForAliasCode(TransCelerate.SDR.Core.Entities.StudyV2.AliasCodeEntity)')
  - [RemoveIdForBioMedicalConcepts(biomedicalConcepts)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForBioMedicalConcepts-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-BiomedicalConceptEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForBioMedicalConcepts(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.BiomedicalConceptEntity})')
  - [RemoveIdForEncounters(encounters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForEncounters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EncounterEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForEncounters(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EncounterEntity})')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity})')
  - [RemoveIdForScheduleTimelines(scheduleTimelines)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForScheduleTimelines-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ScheduleTimelineEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForScheduleTimelines(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ScheduleTimelineEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity})')
  - [RemoveStudyDesignElements(sections,studyDesigns,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV2-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto)')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.HelperV2.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
- [HelperV3](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3')
  - [AreValidStudyDesignElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.GetAuditTrail(System.String)')
  - [GetChangedValues(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.GetChangedValues(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.GetChangedValuesForStudyComparison(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForActivities(activities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForActivities-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ActivityEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForActivities(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ActivityEntity})')
  - [RemoveIdForAliasCode(aliasCode)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForAliasCode-TransCelerate-SDR-Core-Entities-StudyV3-AliasCodeEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForAliasCode(TransCelerate.SDR.Core.Entities.StudyV3.AliasCodeEntity)')
  - [RemoveIdForBioMedicalConcepts(biomedicalConcepts)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForBioMedicalConcepts-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-BiomedicalConceptEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForBioMedicalConcepts(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.BiomedicalConceptEntity})')
  - [RemoveIdForEncounters(encounters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForEncounters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EncounterEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForEncounters(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EncounterEntity})')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity})')
  - [RemoveIdForScheduleTimelines(scheduleTimelines)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForScheduleTimelines-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ScheduleTimelineEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForScheduleTimelines(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ScheduleTimelineEntity})')
  - [RemoveIdForStudyArms(studyArms)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyArms-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyArmEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyArms(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyArmEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity})')
  - [RemoveIdForStudyElements(studyElements)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyElements-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyElementEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyElements(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyElementEntity})')
  - [RemoveIdForStudyEpochs(studyEpochs)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyEpochs-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyEpochEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyEpochs(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyEpochEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity})')
  - [RemoveStudyDesignElements(sections,studyDesigns,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV3-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto)')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.HelperV3.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [HelperV4](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4')
  - [AreValidStudyDesignElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.GetAuditTrail(System.String)')
  - [GetChangedValues(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.GetChangedValues(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.GetChangedValuesForStudyComparison(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveStudyDesignElements(sections,studyDesigns,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV4-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.HelperV4.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto)')
- [HelperV5](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5')
  - [AreValidStudyDesignElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofElementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.GetAuditTrail(System.String)')
  - [GetChangedValues(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.GetChangedValues(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.GetChangedValuesForStudyComparison(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveStudyDesignElements(sections,studyDesigns,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV5-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.HelperV5.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto)')
- [HttpContextResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper')
  - [Response(context,response)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper-Response-Microsoft-AspNetCore-Http-HttpContext,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper.Response(Microsoft.AspNetCore.Http.HttpContext,System.String)')
- [IHelperV2](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2')
  - [AreValidStudyDesignElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.GetAuditTrail(System.String)')
  - [GetSerializerSettingsForCamelCasing()](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-GetSerializerSettingsForCamelCasing 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.GetSerializerSettingsForCamelCasing')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity})')
  - [RemoveStudyDesignElements(sections,studyDTO,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV2-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto)')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2.IHelperV2.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
- [IHelperV3](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3')
  - [AreValidStudyDesignElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.GetAuditTrail(System.String)')
  - [GetSerializerSettingsForCamelCasing()](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-GetSerializerSettingsForCamelCasing 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.GetSerializerSettingsForCamelCasing')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity})')
  - [RemoveStudyDesignElements(sections,studyDTO,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV3-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto)')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3.IHelperV3.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [IHelperV4](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4')
  - [AreValidStudyDesignElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.GetAuditTrail(System.String)')
  - [GetSerializerSettingsForCamelCasing()](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-GetSerializerSettingsForCamelCasing 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.GetSerializerSettingsForCamelCasing')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveStudyDesignElements(sections,studyDTO,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV4-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4.IHelperV4.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto)')
- [IHelperV5](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5')
  - [AreValidStudyDesignElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-AreValidStudyDesignElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.AreValidStudyDesignElements(System.String,System.String[]@)')
  - [AreValidStudyElements(listofelements,listofelementsArray)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-AreValidStudyElements-System-String,System-String[]@- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.AreValidStudyElements(System.String,System.String[]@)')
  - [GetAuditTrail(usdmVersion)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.GetAuditTrail(System.String)')
  - [GetSerializerSettingsForCamelCasing()](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-GetSerializerSettingsForCamelCasing 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.GetSerializerSettingsForCamelCasing')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity,TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveStudyDesignElements(sections,studyDTO,study_uuid)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV5-StudyDesignDto},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.RemoveStudyDesignElements(System.String[],System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto},System.String)')
  - [RemoveStudyElements(sections,studyDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5.IHelperV5.RemoveStudyElements(System.String[],TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto)')
- [IdGenerator](#T-TransCelerate-SDR-Core-Utilities-Helpers-IdGenerator 'TransCelerate.SDR.Core.Utilities.Helpers.IdGenerator')
  - [GenerateId()](#M-TransCelerate-SDR-Core-Utilities-Helpers-IdGenerator-GenerateId 'TransCelerate.SDR.Core.Utilities.Helpers.IdGenerator.GenerateId')
- [LogHelper](#T-TransCelerate-SDR-Core-Utilities-LogHelper 'TransCelerate.SDR.Core.Utilities.LogHelper')
  - [LogCriitical(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogCriitical-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogCriitical(System.String)')
  - [LogDebug(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogDebug-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogDebug(System.String)')
  - [LogError(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogError-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogError(System.String)')
  - [LogInformation(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogInformation-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogInformation(System.String)')
  - [LogTrace(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogTrace-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogTrace(System.String)')
  - [LogWarning(message)](#M-TransCelerate-SDR-Core-Utilities-LogHelper-LogWarning-System-String- 'TransCelerate.SDR.Core.Utilities.LogHelper.LogWarning(System.String)')
- [Permissions](#T-TransCelerate-SDR-Core-Utilities-Permissions 'TransCelerate.SDR.Core.Utilities.Permissions')
- [Route](#T-TransCelerate-SDR-Core-Utilities-Common-Route 'TransCelerate.SDR.Core.Utilities.Common.Route')
- [RuleValidationErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-RuleValidationErrorModel 'TransCelerate.SDR.Core.ErrorModels.RuleValidationErrorModel')
- [ScheduledInstanceType](#T-TransCelerate-SDR-Core-Utilities-ScheduledInstanceType 'TransCelerate.SDR.Core.Utilities.ScheduledInstanceType')
- [SearchTitleResponseDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleResponseDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleResponseDto')
- [SortOrder](#T-TransCelerate-SDR-Core-Utilities-SortOrder 'TransCelerate.SDR.Core.Utilities.SortOrder')
- [SplitStringIntoArrayHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper 'TransCelerate.SDR.Core.Utilities.Helpers.SplitStringIntoArrayHelper')
  - [SplitString(value,index)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper-SplitString-System-String,System-Int32- 'TransCelerate.SDR.Core.Utilities.Helpers.SplitStringIntoArrayHelper.SplitString(System.String,System.Int32)')
- [StartupLib](#T-TransCelerate-SDR-Core-AppSettings-StartupLib 'TransCelerate.SDR.Core.AppSettings.StartupLib')
  - [SetConstants(config)](#M-TransCelerate-SDR-Core-AppSettings-StartupLib-SetConstants-Microsoft-Extensions-Configuration-IConfiguration- 'TransCelerate.SDR.Core.AppSettings.StartupLib.SetConstants(Microsoft.Extensions.Configuration.IConfiguration)')
- [StudyDesignSections](#T-TransCelerate-SDR-Core-Utilities-StudyDesignSections 'TransCelerate.SDR.Core.Utilities.StudyDesignSections')
- [StudyHistoryResponseDto](#T-TransCelerate-SDR-Core-DTO-Common-StudyHistoryResponseDto 'TransCelerate.SDR.Core.DTO.Common.StudyHistoryResponseDto')
- [StudySectionTypes](#T-TransCelerate-SDR-Core-Utilities-StudySectionTypes 'TransCelerate.SDR.Core.Utilities.StudySectionTypes')
- [StudySections](#T-TransCelerate-SDR-Core-Utilities-StudySections 'TransCelerate.SDR.Core.Utilities.StudySections')
- [SuccessResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-SuccessResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.SuccessResponseHelper')
  - [ValidationSuccess()](#M-TransCelerate-SDR-Core-Utilities-Helpers-SuccessResponseHelper-ValidationSuccess-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.SuccessResponseHelper.ValidationSuccess(System.String)')
- [UsageReportQueryHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper 'TransCelerate.SDR.Core.Utilities.Helpers.UsageReportQueryHelper')
  - [FormattedQuery(reportBodyParameters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper-FormattedQuery-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters- 'TransCelerate.SDR.Core.Utilities.Helpers.UsageReportQueryHelper.FormattedQuery(TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters)')
- [ValidationErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel 'TransCelerate.SDR.Core.ErrorModels.ValidationErrorModel')

<a name='T-TransCelerate-SDR-Core-Filters-ActionFilter'></a>
## ActionFilter `type`

##### Namespace

TransCelerate.SDR.Core.Filters

##### Summary

This class is an action filter which will be executed before and after an action is performed

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper'></a>
## ApiBehaviourOptionsHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class helps to response for the different Api behaviours

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper-ModelStateResponse-Microsoft-AspNetCore-Mvc-ActionContext-'></a>
### ModelStateResponse(context) `method`

##### Summary

This method helps to format the error response for invalid input and conformance error

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Microsoft.AspNetCore.Mvc.ActionContext](#T-Microsoft-AspNetCore-Mvc-ActionContext 'Microsoft.AspNetCore.Mvc.ActionContext') | Action Context |

<a name='T-TransCelerate-SDR-Core-Utilities-Common-Config'></a>
## Config `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Common

##### Summary

This class holds the value from keyvault which are fetched at runtime

<a name='T-TransCelerate-SDR-Core-Utilities-Common-Constants'></a>
## Constants `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Common

##### Summary

This class holds all the constant strings used in the application

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper'></a>
## DateValidationHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper-IsValid-System-Object-'></a>
### IsValid(value) `method`

##### Summary

Validator for Date

##### Returns

`true` If passed value is a valid date

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Value which needed to be checked for valid date |

<a name='T-TransCelerate-SDR-Core-ErrorModels-ErrorModel'></a>
## ErrorModel `type`

##### Namespace

TransCelerate.SDR.Core.ErrorModels

##### Summary

This class is a Model for error

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper'></a>
## ErrorResponseHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

Response Helper for Errors

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-String-'></a>
### BadRequest(message) `method`

##### Summary

Resposne Helper When there is Bad Request

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When there is Bad Request

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-Object,System-String-'></a>
### BadRequest(validationProblemDetails,message) `method`

##### Summary

Resposne Helper When there is Conformance Error or Invalid Inpt

##### Returns

A [ValidationErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel 'TransCelerate.SDR.Core.ErrorModels.ValidationErrorModel') When there is Conformance Error or Invalid Inpt

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| validationProblemDetails | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object for holding validation errors |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-ErrorResponseModel-System-Exception-'></a>
### ErrorResponseModel(exception) `method`

##### Summary

Resposne Helper When there is an exception

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When there is an exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-GatewayError'></a>
### GatewayError() `method`

##### Summary

Resposne Helper When there is Gateway Error

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When there is Gateway Error

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-InternalServerError-System-String-'></a>
### InternalServerError(message) `method`

##### Summary

Resposne Helper When there is a Internal server error

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-MethodNotAllowed-System-String-'></a>
### MethodNotAllowed(message) `method`

##### Summary

Resposne Helper When specific method for an API is not called. Ex: When a GET method is called with a POST request.

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When specific method for an API is not called

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-NotFound-System-String-'></a>
### NotFound(message) `method`

##### Summary

Resposne Helper When the resource is Not Found

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When the resource is Not Found

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-ValidationBadRequest-System-Object,System-Object,System-String-'></a>
### ValidationBadRequest(validationErrorDetails,validationWarningDetails,message) `method`

##### Summary

Validation Response Helper When there is Conformance Rule Error or Invalid Input

##### Returns

A [ValidationErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel 'TransCelerate.SDR.Core.ErrorModels.ValidationErrorModel') When there is Conformance Rule Error or Invalid Input

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| validationErrorDetails | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object for holding validation errors |
| validationWarningDetails | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object for holding validation warnings |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper'></a>
## FromDateToDateHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This is a helper class to format from and to date for the endpoints

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper-GetFromAndToDate-System-DateTime,System-DateTime,System-Int32-'></a>
### GetFromAndToDate(fromDate,toDate,range) `method`

##### Summary

This method helps to return formatted from and to date

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | From Date |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | To Date |
| range | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Date Range for which the from date have to be formatted (used for studyHistory endpoint). |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2'></a>
## HelperV2 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2

##### Summary

This class is used as a helper for different funtionalities

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### GetChangedValues(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForActivities-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ActivityEntity}-'></a>
### RemoveIdForActivities(activities) `method`

##### Summary

Remove uuid for Activities

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ActivityEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ActivityEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForAliasCode-TransCelerate-SDR-Core-Entities-StudyV2-AliasCodeEntity-'></a>
### RemoveIdForAliasCode(aliasCode) `method`

##### Summary

Remove uuid for AliasCode

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasCode | [TransCelerate.SDR.Core.Entities.StudyV2.AliasCodeEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-AliasCodeEntity 'TransCelerate.SDR.Core.Entities.StudyV2.AliasCodeEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForBioMedicalConcepts-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-BiomedicalConceptEntity}-'></a>
### RemoveIdForBioMedicalConcepts(biomedicalConcepts) `method`

##### Summary

Remove uuid for Biomedical Concepts

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| biomedicalConcepts | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.BiomedicalConceptEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.BiomedicalConceptEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForEncounters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EncounterEntity}-'></a>
### RemoveIdForEncounters(encounters) `method`

##### Summary

Remove uuid for Encounters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encounters | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EncounterEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EncounterEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForScheduleTimelines-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ScheduleTimelineEntity}-'></a>
### RemoveIdForScheduleTimelines(scheduleTimelines) `method`

##### Summary

Remove uuid for Schedule Timelines

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scheduleTimelines | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ScheduleTimelineEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ScheduleTimelineEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV2-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDesigns,study_uuid) `method`

##### Summary

Remove the study design elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-HelperV2-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3'></a>
## HelperV3 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3

##### Summary

This class is used as a helper for different funtionalities

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### GetChangedValues(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForActivities-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ActivityEntity}-'></a>
### RemoveIdForActivities(activities) `method`

##### Summary

Remove uuid for Activities

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| activities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ActivityEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ActivityEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForAliasCode-TransCelerate-SDR-Core-Entities-StudyV3-AliasCodeEntity-'></a>
### RemoveIdForAliasCode(aliasCode) `method`

##### Summary

Remove uuid for AliasCode

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasCode | [TransCelerate.SDR.Core.Entities.StudyV3.AliasCodeEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-AliasCodeEntity 'TransCelerate.SDR.Core.Entities.StudyV3.AliasCodeEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForBioMedicalConcepts-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-BiomedicalConceptEntity}-'></a>
### RemoveIdForBioMedicalConcepts(biomedicalConcepts) `method`

##### Summary

Remove uuid for Biomedical Concepts

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| biomedicalConcepts | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.BiomedicalConceptEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.BiomedicalConceptEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForEncounters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EncounterEntity}-'></a>
### RemoveIdForEncounters(encounters) `method`

##### Summary

Remove uuid for Encounters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encounters | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EncounterEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EncounterEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForScheduleTimelines-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ScheduleTimelineEntity}-'></a>
### RemoveIdForScheduleTimelines(scheduleTimelines) `method`

##### Summary

Remove uuid for Schedule Timelines

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scheduleTimelines | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ScheduleTimelineEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ScheduleTimelineEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyArms-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyArmEntity}-'></a>
### RemoveIdForStudyArms(studyArms) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyArms | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyArmEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyArmEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyElements-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyElementEntity}-'></a>
### RemoveIdForStudyElements(studyElements) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyElements | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyElementEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyEpochs-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyEpochEntity}-'></a>
### RemoveIdForStudyEpochs(studyEpochs) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyEpochs | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyEpochEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyEpochEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV3-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDesigns,study_uuid) `method`

##### Summary

Remove the study design elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-HelperV3-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4'></a>
## HelperV4 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4

##### Summary

This class is used as a helper for different funtionalities

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### GetChangedValues(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV4-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDesigns,study_uuid) `method`

##### Summary

Remove the study design elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-HelperV4-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5'></a>
## HelperV5 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5

##### Summary

This class is used as a helper for different funtionalities

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofElementsArray) `method`

##### Summary

Check whether the the input list of study elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofElementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetChangedValues-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### GetChangedValues(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-GetChangedValuesForStudyComparison-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### GetChangedValuesForStudyComparison(currentStudyVersion,previousStudyVersion) `method`

##### Summary

Get the differences between two studies

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Current study version |
| previousStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Previous study version |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV5-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDesigns,study_uuid) `method`

##### Summary

Remove the study design elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-HelperV5-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper'></a>
## HttpContextResponseHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is used to format the error messages in the response

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper-Response-Microsoft-AspNetCore-Http-HttpContext,System-String-'></a>
### Response(context,response) `method`

##### Summary

This method is used to format the error messages in the response

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Microsoft.AspNetCore.Http.HttpContext](#T-Microsoft-AspNetCore-Http-HttpContext 'Microsoft.AspNetCore.Http.HttpContext') | HttpContext |
| response | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Response string |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2'></a>
## IHelperV2 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-GetSerializerSettingsForCamelCasing'></a>
### GetSerializerSettingsForCamelCasing() `method`

##### Summary

JSON Serializer for camel casing

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV2-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV2.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV2-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDTO,study_uuid) `method`

##### Summary

Remove studyDesign elements which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV2-IHelperV2-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3'></a>
## IHelperV3 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-GetSerializerSettingsForCamelCasing'></a>
### GetSerializerSettingsForCamelCasing() `method`

##### Summary

JSON Serializer for camel casing

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV3-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV3.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV3-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDTO,study_uuid) `method`

##### Summary

Remove studyDesign elements which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV3-IHelperV3-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4'></a>
## IHelperV4 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-GetSerializerSettingsForCamelCasing'></a>
### GetSerializerSettingsForCamelCasing() `method`

##### Summary

JSON Serializer for camel casing

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV4-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDTO,study_uuid) `method`

##### Summary

Remove studyDesign elements which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV4.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV4-IHelperV4-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5'></a>
## IHelperV5 `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-AreValidStudyDesignElements-System-String,System-String[]@-'></a>
### AreValidStudyDesignElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of study design elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-AreValidStudyElements-System-String,System-String[]@-'></a>
### AreValidStudyElements(listofelements,listofelementsArray) `method`

##### Summary

Check whether the the input list of elements are valid or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| listofelementsArray | [System.String[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[]@ 'System.String[]@') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-GetAuditTrail-System-String-'></a>
### GetAuditTrail(usdmVersion) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-GetSerializerSettingsForCamelCasing'></a>
### GetSerializerSettingsForCamelCasing() `method`

##### Summary

JSON Serializer for camel casing

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity,TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-RemoveStudyDesignElements-System-String[],System-Collections-Generic-List{TransCelerate-SDR-Core-DTO-StudyV5-StudyDesignDto},System-String-'></a>
### RemoveStudyDesignElements(sections,studyDTO,study_uuid) `method`

##### Summary

Remove studyDesign elements which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.DTO.StudyV5.StudyDesignDto}') |  |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV5-IHelperV5-RemoveStudyElements-System-String[],TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto-'></a>
### RemoveStudyElements(sections,studyDTO) `method`

##### Summary

Remove the study elemets which are not requested

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto') |  |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-IdGenerator'></a>
## IdGenerator `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-IdGenerator-GenerateId'></a>
### GenerateId() `method`

##### Summary

Used for generating UUID

##### Returns

A new [Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid')

##### Parameters

This method has no parameters.

<a name='T-TransCelerate-SDR-Core-Utilities-LogHelper'></a>
## LogHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogCriitical-System-String-'></a>
### LogCriitical(message) `method`

##### Summary

Logs Critical Failures

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogDebug-System-String-'></a>
### LogDebug(message) `method`

##### Summary

Logs When debug logging is is added

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogError-System-String-'></a>
### LogError(message) `method`

##### Summary

Logs Error

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogInformation-System-String-'></a>
### LogInformation(message) `method`

##### Summary

Logs Information

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogTrace-System-String-'></a>
### LogTrace(message) `method`

##### Summary

Logs Traces

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='M-TransCelerate-SDR-Core-Utilities-LogHelper-LogWarning-System-String-'></a>
### LogWarning(message) `method`

##### Summary

Logs Warning

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message will be logged |

<a name='T-TransCelerate-SDR-Core-Utilities-Permissions'></a>
## Permissions `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Enums for Permissions

<a name='T-TransCelerate-SDR-Core-Utilities-Common-Route'></a>
## Route `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Common

##### Summary

This class holds list of routes for all the endpoints

<a name='T-TransCelerate-SDR-Core-ErrorModels-RuleValidationErrorModel'></a>
## RuleValidationErrorModel `type`

##### Namespace

TransCelerate.SDR.Core.ErrorModels

##### Summary

This class is a model for rule validation errors

<a name='T-TransCelerate-SDR-Core-Utilities-ScheduledInstanceType'></a>
## ScheduledInstanceType `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Enum for Scheduled Instance Types

<a name='T-TransCelerate-SDR-Core-DTO-Common-SearchTitleResponseDto'></a>
## SearchTitleResponseDto `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Common

##### Summary

This class is a DTO for response of Search StudyTitle Endpoint

<a name='T-TransCelerate-SDR-Core-Utilities-SortOrder'></a>
## SortOrder `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Enums for Permissions

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper'></a>
## SplitStringIntoArrayHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper-SplitString-System-String,System-Int32-'></a>
### SplitString(value,index) `method`

##### Summary

This method helps to split the array based on index value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String which needs to be split |
| index | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The number of parts the string have to be split into |

<a name='T-TransCelerate-SDR-Core-AppSettings-StartupLib'></a>
## StartupLib `type`

##### Namespace

TransCelerate.SDR.Core.AppSettings

<a name='M-TransCelerate-SDR-Core-AppSettings-StartupLib-SetConstants-Microsoft-Extensions-Configuration-IConfiguration-'></a>
### SetConstants(config) `method`

##### Summary

Get From appsettings.json at runtime

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') | IConfiguration parameter |

<a name='T-TransCelerate-SDR-Core-Utilities-StudyDesignSections'></a>
## StudyDesignSections `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Enums for StudyDesign Sections

<a name='T-TransCelerate-SDR-Core-DTO-Common-StudyHistoryResponseDto'></a>
## StudyHistoryResponseDto `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Common

##### Summary

This class is a DTO for response of GET Study History Endpoint

<a name='T-TransCelerate-SDR-Core-Utilities-StudySectionTypes'></a>
## StudySectionTypes `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Sections Types for adding in CurrentSections: SectionType

<a name='T-TransCelerate-SDR-Core-Utilities-StudySections'></a>
## StudySections `type`

##### Namespace

TransCelerate.SDR.Core.Utilities

##### Summary

Enums for Study Level Sections

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-SuccessResponseHelper'></a>
## SuccessResponseHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SuccessResponseHelper-ValidationSuccess-System-String-'></a>
### ValidationSuccess() `method`

##### Summary

Response Helper for successful validation

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When there is an Unauthorized Access

##### Parameters

This method has no parameters.

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper'></a>
## UsageReportQueryHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This is a helper class to format the KQL query

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper-FormattedQuery-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters-'></a>
### FormattedQuery(reportBodyParameters) `method`

##### Summary

This method helps to format the KQL query for system usage report

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reportBodyParameters | [TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters](#T-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters 'TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters') | Body parameters for usage report |

<a name='T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel'></a>
## ValidationErrorModel `type`

##### Namespace

TransCelerate.SDR.Core.ErrorModels

##### Summary

This class is a Model for validation errors
