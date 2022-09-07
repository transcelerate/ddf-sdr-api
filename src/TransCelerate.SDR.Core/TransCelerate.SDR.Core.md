<a name='assembly'></a>
# TransCelerate.SDR.Core

## Contents

- [ActionFilter](#T-TransCelerate-SDR-Core-Filters-ActionFilter 'TransCelerate.SDR.Core.Filters.ActionFilter')
- [AllowAnonymousFilter](#T-TransCelerate-SDR-Core-Filters-AllowAnonymousFilter 'TransCelerate.SDR.Core.Filters.AllowAnonymousFilter')
- [ApiBehaviourOptionsHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper 'TransCelerate.SDR.Core.Utilities.Helpers.ApiBehaviourOptionsHelper')
  - [ModelStateResponse(context)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ApiBehaviourOptionsHelper-ModelStateResponse-Microsoft-AspNetCore-Mvc-ActionContext- 'TransCelerate.SDR.Core.Utilities.Helpers.ApiBehaviourOptionsHelper.ModelStateResponse(Microsoft.AspNetCore.Mvc.ActionContext)')
- [AudiTrailResponseDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-AudiTrailResponseDto 'TransCelerate.SDR.Core.DTO.StudyV1.AudiTrailResponseDto')
- [Config](#T-TransCelerate-SDR-Core-Utilities-Common-Config 'TransCelerate.SDR.Core.Utilities.Common.Config')
- [Constants](#T-TransCelerate-SDR-Core-Utilities-Common-Constants 'TransCelerate.SDR.Core.Utilities.Common.Constants')
- [DateValidationHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper 'TransCelerate.SDR.Core.Utilities.Helpers.DateValidationHelper')
  - [IsValid(value)](#M-TransCelerate-SDR-Core-Utilities-Helpers-DateValidationHelper-IsValid-System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.DateValidationHelper.IsValid(System.Object)')
- [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel')
- [ErrorResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper')
  - [BadRequest(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.BadRequest(System.String)')
  - [BadRequest(validationProblemDetails,message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-BadRequest-System-Object,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.BadRequest(System.Object,System.String)')
  - [ErrorResponseModel(exception)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-ErrorResponseModel-System-Exception- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.ErrorResponseModel(System.Exception)')
  - [Forbidden(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-Forbidden-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.Forbidden(System.String)')
  - [GatewayError()](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-GatewayError 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.GatewayError')
  - [InternalServerError(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-InternalServerError-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.InternalServerError(System.String)')
  - [MethodNotAllowed(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-MethodNotAllowed-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.MethodNotAllowed(System.String)')
  - [NotFound(message)](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-NotFound-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.NotFound(System.String)')
  - [UnAuthorizedAccess()](#M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-UnAuthorizedAccess-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.ErrorResponseHelper.UnAuthorizedAccess(System.String)')
- [FromDateToDateHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper 'TransCelerate.SDR.Core.Utilities.Helpers.FromDateToDateHelper')
  - [GetFromAndToDate(fromDate,toDate,range)](#M-TransCelerate-SDR-Core-Utilities-Helpers-FromDateToDateHelper-GetFromAndToDate-System-DateTime,System-DateTime,System-Int32- 'TransCelerate.SDR.Core.Utilities.Helpers.FromDateToDateHelper.GetFromAndToDate(System.DateTime,System.DateTime,System.Int32)')
- [GetClinicalStudyDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO')
  - [objectives](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-objectives 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.objectives')
  - [studyDesigns](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyDesigns 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyDesigns')
  - [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')
  - [studyIdentifiers](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyIdentifiers 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyIdentifiers')
  - [studyIndications](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyIndications 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyIndications')
  - [studyPhase](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyPhase 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyPhase')
  - [studyProtocolReferences](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyProtocolReferences 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyProtocolReferences')
  - [studyStatus](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyStatus 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyStatus')
  - [studyTag](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyTag 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyTag')
  - [studyTitle](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyTitle 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyTitle')
  - [studyType](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyType 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyType')
- [GetStudyAuditDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO')
  - [auditTrail](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO-auditTrail 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO.auditTrail')
  - [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO.studyId')
- [GetStudyDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyDTO')
  - [auditTrail](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO-auditTrail 'TransCelerate.SDR.Core.DTO.Study.GetStudyDTO.auditTrail')
  - [clinicalStudy](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO-clinicalStudy 'TransCelerate.SDR.Core.DTO.Study.GetStudyDTO.clinicalStudy')
- [GetStudyDesignsDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO')
  - [investigationalInterventions](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-investigationalInterventions 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.investigationalInterventions')
  - [plannedWorkflows](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-plannedWorkflows 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.plannedWorkflows')
  - [studyCells](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyCells 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyCells')
  - [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')
  - [studyPopulations](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyPopulations 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyPopulations')
  - [trialIntentType](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-trialIntentType 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.trialIntentType')
  - [trialType](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-trialType 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.trialType')
- [GetStudyHistoryResponseDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyHistoryResponseDTO')
  - [study](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO-study 'TransCelerate.SDR.Core.DTO.Study.GetStudyHistoryResponseDTO.study')
- [GetStudySectionsDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO')
  - [objectives](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-objectives 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.objectives')
  - [studyDesigns](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyDesigns 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyDesigns')
  - [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyId')
  - [studyIndications](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyIndications 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyIndications')
  - [studyVersion](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyVersion 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyVersion')
- [GroupFilters](#T-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters 'TransCelerate.SDR.Core.Utilities.Helpers.GroupFilters')
  - [GetFilterValues(groups,field)](#M-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters-GetFilterValues-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.GroupFilters.GetFilterValues(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},System.String)')
  - [GetGroupFilters(groups)](#M-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters-GetGroupFilters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.GroupFilters.GetGroupFilters(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity})')
- [Helper](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper')
  - [CheckForCodeSection(incomingCodes,existingCodes)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForCodeSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForCodeSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity})')
  - [CheckForDefinedProceduresSection(incomingDefinedProcedures,exisitingDefinedProcedures)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForDefinedProceduresSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForDefinedProceduresSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity})')
  - [CheckForEncounterListSection(incomingEncounters,existingEncounters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForEncounterListSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EncounterEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EncounterEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForEncounterListSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity})')
  - [CheckForIntercurrentEventsSection(incomingInterCurrentEvents,exisitingInterCurrentEvents)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForIntercurrentEventsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForIntercurrentEventsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity})')
  - [CheckForInvestigationalInterventionsSection(incomingInvestigationalInterventions,existingInvestigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForInvestigationalInterventionsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForInvestigationalInterventionsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [CheckForSections(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForSections-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForSections(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity,TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [CheckForStudyCellsSection(incomingStudyCells,existingStudyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyCellsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyCellsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [CheckForStudyDataCollectionSection(incomingStudyDataCollections,existingStudyDataCollections)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDataCollectionSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyDataCollectionSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity})')
  - [CheckForStudyDesignPopulationsSection(incomingStudyDesignPopulations,existingStudyDesignPopulations)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDesignPopulationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyDesignPopulationsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity})')
  - [CheckForStudyDesignSection(incomingStudyDesigns,existingStudyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDesignSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyDesignSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [CheckForStudyElementsSection(incomingStudyElements,existingStudyElements)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyElementsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyElementsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity})')
  - [CheckForStudyEstimandSection(incomingEstimands,existingEstimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyEstimandSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyEstimandSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [CheckForStudyIdentifierSection(incomingStudyIdentifiers,existingStudyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyIdentifierSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyIdentifierSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [CheckForStudyIndicationsSection(incomingIndications,exisitingIndications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyIndicationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyIndicationsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [CheckForStudyObjectivesEndpointsSection(incomingEndpoints,existingEndpoints)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyObjectivesEndpointsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyObjectivesEndpointsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity})')
  - [CheckForStudyObjectivesSection(incomingObjectives,existingObjectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyObjectivesSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyObjectivesSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [CheckForStudyProtocolSection(incomingStudyProtocolVersions,existingStudyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyProtocolSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyProtocolSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [CheckForStudyWorkflowItemsSection(incomingWorkflowItems,existingWorkflowItems)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyWorkflowItemsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyWorkflowItemsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity})')
  - [CheckForStudyWorkflowSection(incomingWorkflows,existingWorkflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyWorkflowSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.CheckForStudyWorkflowSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [GenerateIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [GenerateIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [GenerateIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [GenerateIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [GenerateIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [GenerateIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [GenerateIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [GenerateIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [GenerateIdForStudyWorkflow(workflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GenerateIdForStudyWorkflow(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [GeneratedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GeneratedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GeneratedSectionId(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [GetAuditTrail(user)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.GetAuditTrail(System.String)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity,TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [RemoveIdForStudyWorkflow(workflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemoveIdForStudyWorkflow(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.Helper.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
- [HttpContextResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper')
  - [Response(context,response)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper-Response-Microsoft-AspNetCore-Http-HttpContext,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper.Response(Microsoft.AspNetCore.Http.HttpContext,System.String)')
- [IHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper')
  - [CheckForCodeSection(incomingCodes,existingCodes)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForCodeSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForCodeSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity})')
  - [CheckForDefinedProceduresSection(incomingDefinedProcedures,exisitingDefinedProcedures)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForDefinedProceduresSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForDefinedProceduresSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity})')
  - [CheckForIntercurrentEventsSection(incomingInterCurrentEvents,exisitingInterCurrentEvents)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForIntercurrentEventsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForIntercurrentEventsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity})')
  - [CheckForInvestigationalInterventionsSection(incomingInvestigationalInterventions,existingInvestigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForInvestigationalInterventionsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForInvestigationalInterventionsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [CheckForSections(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForSections-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForSections(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity,TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [CheckForStudyCellsSection(incomingStudyCells,existingStudyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyCellsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyCellsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [CheckForStudyDataCollectionSection(incomingStudyDataCollections,existingStudyDataCollections)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDataCollectionSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyDataCollectionSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity})')
  - [CheckForStudyDesignPopulationsSection(incomingStudyDesignPopulations,existingStudyDesignPopulations)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDesignPopulationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyDesignPopulationsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity})')
  - [CheckForStudyDesignSection(incomingStudyDesigns,existingStudyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDesignSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyDesignSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [CheckForStudyElementsSection(incomingStudyElements,existingStudyElements)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyElementsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyElementsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity})')
  - [CheckForStudyEstimandSection(incomingEstimands,existingEstimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyEstimandSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyEstimandSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [CheckForStudyIdentifierSection(incomingStudyIdentifiers,existingStudyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyIdentifierSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyIdentifierSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [CheckForStudyIndicationsSection(incomingIndications,exisitingIndications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyIndicationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyIndicationsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [CheckForStudyObjectivesEndpointsSection(incomingEndpoints,existingEndpoints)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyObjectivesEndpointsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyObjectivesEndpointsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity})')
  - [CheckForStudyObjectivesSection(incomingObjectives,existingObjectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyObjectivesSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyObjectivesSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [CheckForStudyProtocolSection(incomingStudyProtocolVersions,existingStudyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyProtocolSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyProtocolSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [CheckForStudyWorkflowItemsSection(incomingWorkflowItems,existingWorkflowItems)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyWorkflowItemsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyWorkflowItemsSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity})')
  - [CheckForStudyWorkflowSection(incomingWorkflows,existingWorkflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyWorkflowSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.CheckForStudyWorkflowSection(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [GenerateIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [GenerateIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [GenerateIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [GenerateIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [GenerateIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [GenerateIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [GenerateIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [GenerateIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [GenerateIdForStudyWorkflow(workflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GenerateIdForStudyWorkflow(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [GeneratedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GeneratedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GeneratedSectionId(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [GetAuditTrail(user)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GetAuditTrail-System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.GetAuditTrail(System.String)')
  - [IsSameStudy(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.IsSameStudy(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity,TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.JsonObjectCheck(System.Object,System.Object)')
  - [RemoveIdForInvestigationalInterventions(investigationalInterventions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForInvestigationalInterventions(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity})')
  - [RemoveIdForStudyCells(studyCells)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyCells(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity})')
  - [RemoveIdForStudyDesign(studyDesigns)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyDesign(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity})')
  - [RemoveIdForStudyEstimand(estimands)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyEstimand(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity})')
  - [RemoveIdForStudyIdentifier(studyIdentifiers)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyIdentifier(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity})')
  - [RemoveIdForStudyIndications(indications)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyIndications(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity})')
  - [RemoveIdForStudyObjectives(objectives)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyObjectives(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity})')
  - [RemoveIdForStudyProtocol(studyProtocolVersions)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyProtocol(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity})')
  - [RemoveIdForStudyWorkflow(workflows)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemoveIdForStudyWorkflow(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity})')
  - [RemovedSectionId(study)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1.IHelper.RemovedSectionId(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
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
- [PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO')
  - [auditTrail](#P-TransCelerate-SDR-Core-DTO-PostStudyDTO-auditTrail 'TransCelerate.SDR.Core.DTO.PostStudyDTO.auditTrail')
  - [clinicalStudy](#P-TransCelerate-SDR-Core-DTO-PostStudyDTO-clinicalStudy 'TransCelerate.SDR.Core.DTO.PostStudyDTO.clinicalStudy')
- [PostStudyElementsCheck](#T-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck')
  - [ActivitySectionCheck(existingActivityEntity,incomingActivityEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-ActivitySectionCheck-TransCelerate-SDR-Core-Entities-Study-ActivityEntity,TransCelerate-SDR-Core-Entities-Study-ActivityEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.ActivitySectionCheck(TransCelerate.SDR.Core.Entities.Study.ActivityEntity,TransCelerate.SDR.Core.Entities.Study.ActivityEntity)')
  - [EncounterSectionCheck(existingEncounterEntity,incomingEncounterEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-EncounterSectionCheck-TransCelerate-SDR-Core-Entities-Study-EncounterEntity,TransCelerate-SDR-Core-Entities-Study-EncounterEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.EncounterSectionCheck(TransCelerate.SDR.Core.Entities.Study.EncounterEntity,TransCelerate.SDR.Core.Entities.Study.EncounterEntity)')
  - [EpochSectionCheck(existingEpochEntity,incomingEpochEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-EpochSectionCheck-TransCelerate-SDR-Core-Entities-Study-EpochEntity,TransCelerate-SDR-Core-Entities-Study-EpochEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.EpochSectionCheck(TransCelerate.SDR.Core.Entities.Study.EpochEntity,TransCelerate.SDR.Core.Entities.Study.EpochEntity)')
  - [InvestigationalInvestigationSectionCheck(existingStudyDesign,existingInvestigationalInterventionEntities,incomingInvestigationalInterventionEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-InvestigationalInvestigationSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-InvestigationalInterventionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.InvestigationalInvestigationSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity})')
  - [ItemSectionCheck(existingItemEntities,incomingItemEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-ItemSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.ItemSectionCheck(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity})')
  - [JsonObjectCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-JsonObjectCheck-System-Object,System-Object- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.JsonObjectCheck(System.Object,System.Object)')
  - [MatrixSectionCheck(existingMatrixEntities,incomingMatrixEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-MatrixSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-MatrixEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-MatrixEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.MatrixSectionCheck(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity})')
  - [PointInTimeSectionCheck(existingPointInTime,incomingPointInTime)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-PointInTimeSectionCheck-TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity,TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.PointInTimeSectionCheck(TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity,TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity)')
  - [RemoveId(studyEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-RemoveId-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.RemoveId(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [RuleSectionCheck(existingRuleEntity,incomingRuleEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-RuleSectionCheck-TransCelerate-SDR-Core-Entities-Study-RuleEntity,TransCelerate-SDR-Core-Entities-Study-RuleEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.RuleSectionCheck(TransCelerate.SDR.Core.Entities.Study.RuleEntity,TransCelerate.SDR.Core.Entities.Study.RuleEntity)')
  - [SectionCheck(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-SectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.SectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyEntity,TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [StudyArmSectionCheck(existingStudyArm,incomingStudyArm)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyArmSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyArmEntity,TransCelerate-SDR-Core-Entities-Study-StudyArmEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyArmSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyArmEntity,TransCelerate.SDR.Core.Entities.Study.StudyArmEntity)')
  - [StudyCellsSectionCheck(existingStudyDesign,existingStudyCellsEntities,incomingStudyCellsEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyCellsSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyCellEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyCellsSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity})')
  - [StudyComparison(incoming,existing)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyComparison-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyComparison(TransCelerate.SDR.Core.Entities.Study.StudyEntity,TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [StudyDataCollectionSectionCheck(existingStudyDataCollectionEntities,incomingStudyDataCollectionEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyDataCollectionSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDataCollectionEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyDataCollectionSectionCheck(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity})')
  - [StudyDesignSectionCheck(existing,existingStudyDesignEntities,incomingStudyDesignEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyDesignSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyDesignSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity})')
  - [StudyElementsSectionCheck(existingStudyElementsEntities,incomingStudyElementsEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyElementsSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyElementEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyElementsSectionCheck(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity})')
  - [StudyEpochSectionCheck(existingStudyEpoch,incomingStudyEpoch)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyEpochSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity,TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyEpochSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity,TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity)')
  - [StudyIndicationSectionCheck(existing,existingStudyIndicationEntities,incomingStudyIndicationEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyIndicationSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyIndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyIndicationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyIndicationSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity})')
  - [StudyObjectivesSectionCheck(existing,existingStudyObjectivesEntities,incomingStudyObjectivesEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyObjectivesSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyObjectivesSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity})')
  - [StudyPlannedWorkFlowSectionCheck(existingStudyDesign,existingPlannedWorkFlowsEntities,incomingPlannedWorkFlowsEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyPlannedWorkFlowSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyPlannedWorkFlowSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity})')
  - [StudyPopulationsSectionCheck(existingStudyDesign,existingStudyPopulationEntities,incomingStudyPopulationEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyPopulationsSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyPopulationEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.StudyPopulationsSectionCheck(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity},System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity})')
  - [WorkFlowItemMatrixSectionCheck(existingWorkFlowItemMatrixEntity,incomingWorkFlowItemMatrixEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-WorkFlowItemMatrixSectionCheck-TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity,TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PostStudyElementsCheck.WorkFlowItemMatrixSectionCheck(TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity,TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity)')
- [PreviousItemNextItemHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper 'TransCelerate.SDR.Core.Utilities.Helpers.PreviousItemNextItemHelper')
  - [GetPreviousNextItems(itemEntities)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper-GetPreviousNextItems-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity}- 'TransCelerate.SDR.Core.Utilities.Helpers.PreviousItemNextItemHelper.GetPreviousNextItems(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity})')
  - [PreviousItemsNextItemsWraper(studyEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper-PreviousItemsNextItemsWraper-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.PreviousItemNextItemHelper.PreviousItemsNextItemsWraper(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
- [RemoveStudySections](#T-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections 'TransCelerate.SDR.Core.Utilities.Helpers.RemoveStudySections')
  - [PostResponseRemoveSections(studyEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-PostResponseRemoveSections-TransCelerate-SDR-Core-DTO-PostStudyDTO- 'TransCelerate.SDR.Core.Utilities.Helpers.RemoveStudySections.PostResponseRemoveSections(TransCelerate.SDR.Core.DTO.PostStudyDTO)')
  - [RemoveSections(sections,getStudySectionsDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-RemoveSections-System-String[],TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO- 'TransCelerate.SDR.Core.Utilities.Helpers.RemoveStudySections.RemoveSections(System.String[],TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO)')
  - [RemoveSectionsForStudyDesign(sections,getStudySectionsDTO)](#M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-RemoveSectionsForStudyDesign-System-String[],TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO- 'TransCelerate.SDR.Core.Utilities.Helpers.RemoveStudySections.RemoveSectionsForStudyDesign(System.String[],TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO)')
- [Route](#T-TransCelerate-SDR-Core-Utilities-Common-Route 'TransCelerate.SDR.Core.Utilities.Common.Route')
- [SearchTitleResponseDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleResponseDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleResponseDto')
- [SectionIdGenerator](#T-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator')
  - [GenerateSectionId(studyEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-GenerateSectionId-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.GenerateSectionId(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [StudyCellsIdGenerator(studyCellEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyCellsIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyCellEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyCellsIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyCellEntity)')
  - [StudyDesignIdGenerator(studyDesignEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyDesignIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyDesignIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity)')
  - [StudyObjectivesIdGenerator(studyObjectivesEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyObjectivesIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyObjectivesIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity)')
  - [StudyPlannedWorkFlowIdGenerator(plannedWorkFlowEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyPlannedWorkFlowIdGenerator-TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity)')
- [SortOrder](#T-TransCelerate-SDR-Core-Utilities-SortOrder 'TransCelerate.SDR.Core.Utilities.SortOrder')
- [SplitStringIntoArrayHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper 'TransCelerate.SDR.Core.Utilities.Helpers.SplitStringIntoArrayHelper')
  - [SplitString(value,index)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SplitStringIntoArrayHelper-SplitString-System-String,System-Int32- 'TransCelerate.SDR.Core.Utilities.Helpers.SplitStringIntoArrayHelper.SplitString(System.String,System.Int32)')
- [StartupLib](#T-TransCelerate-SDR-Core-AppSettings-StartupLib 'TransCelerate.SDR.Core.AppSettings.StartupLib')
  - [SetConstants(config)](#M-TransCelerate-SDR-Core-AppSettings-StartupLib-SetConstants-Microsoft-Extensions-Configuration-IConfiguration- 'TransCelerate.SDR.Core.AppSettings.StartupLib.SetConstants(Microsoft.Extensions.Configuration.IConfiguration)')
- [StudyDesignSections](#T-TransCelerate-SDR-Core-Utilities-StudyDesignSections 'TransCelerate.SDR.Core.Utilities.StudyDesignSections')
- [StudyHistoryResponseDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyHistoryResponseDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyHistoryResponseDto')
- [StudySectionTypes](#T-TransCelerate-SDR-Core-Utilities-StudySectionTypes 'TransCelerate.SDR.Core.Utilities.StudySectionTypes')
- [StudySections](#T-TransCelerate-SDR-Core-Utilities-StudySections 'TransCelerate.SDR.Core.Utilities.StudySections')
- [UsageReportQueryHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper 'TransCelerate.SDR.Core.Utilities.Helpers.UsageReportQueryHelper')
  - [FormattedQuery(reportBodyParameters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-UsageReportQueryHelper-FormattedQuery-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters- 'TransCelerate.SDR.Core.Utilities.Helpers.UsageReportQueryHelper.FormattedQuery(TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters)')
- [UserGroupSortingHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper 'TransCelerate.SDR.Core.Utilities.Helpers.UserGroupSortingHelper')
  - [OrderGroups(groupDetails,userGroupsQueryParameters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper-OrderGroups-System-Collections-Generic-IEnumerable{TransCelerate-SDR-Core-Entities-UserGroups-GroupDetailsEntity},TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Core.Utilities.Helpers.UserGroupSortingHelper.OrderGroups(System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.Entities.UserGroups.GroupDetailsEntity},TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [OrderUsers(users,userGroupsQueryParameters)](#M-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper-OrderUsers-System-Collections-Generic-IEnumerable{TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO},TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Core.Utilities.Helpers.UserGroupSortingHelper.OrderUsers(System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO},TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
- [ValidationErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel 'TransCelerate.SDR.Core.ErrorModels.ValidationErrorModel')

<a name='T-TransCelerate-SDR-Core-Filters-ActionFilter'></a>
## ActionFilter `type`

##### Namespace

TransCelerate.SDR.Core.Filters

##### Summary

This class is an action filter which will be executed before and after an action is performed

<a name='T-TransCelerate-SDR-Core-Filters-AllowAnonymousFilter'></a>
## AllowAnonymousFilter `type`

##### Namespace

TransCelerate.SDR.Core.Filters

##### Summary

This authorisation handler will bypass all requirements

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

<a name='T-TransCelerate-SDR-Core-DTO-StudyV1-AudiTrailResponseDto'></a>
## AudiTrailResponseDto `type`

##### Namespace

TransCelerate.SDR.Core.DTO.StudyV1

##### Summary

This class is a DTO for response of GET Audit Trail Endpoint

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

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-Forbidden-System-String-'></a>
### Forbidden(message) `method`

##### Summary

Resposne Helper When the user is accessing a restricted data or APi

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message for error response |

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

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-ErrorResponseHelper-UnAuthorizedAccess-System-String-'></a>
### UnAuthorizedAccess() `method`

##### Summary

Resposne Helper When there is an Unauthorized Access

##### Returns

A [ErrorModel](#T-TransCelerate-SDR-Core-ErrorModels-ErrorModel 'TransCelerate.SDR.Core.ErrorModels.ErrorModel') When there is an Unauthorized Access

##### Parameters

This method has no parameters.

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

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO'></a>
## GetClinicalStudyDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for all elements of clinicalStudy

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-objectives'></a>
### objectives `property`

##### Summary

This property holds the List of Study Objectives for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyDesigns'></a>
### studyDesigns `property`

##### Summary

This property holds the List of Study Designs for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId'></a>
### studyId `property`

##### Summary

This property holds the value of Study ID

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyIdentifiers'></a>
### studyIdentifiers `property`

##### Summary

This property holds the List of Study Identifiers for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyIndications'></a>
### studyIndications `property`

##### Summary

This property holds the List of Study Indications for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyPhase'></a>
### studyPhase `property`

##### Summary

This property holds the value of Study Phase for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyProtocolReferences'></a>
### studyProtocolReferences `property`

##### Summary

This property holds the List of Study Protocol References for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyStatus'></a>
### studyStatus `property`

##### Summary

This property holds the value of Study Status for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyTag'></a>
### studyTag `property`

##### Summary

This property holds the value of Study Tag for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyTitle'></a>
### studyTitle `property`

##### Summary

This property holds the value of Study Title for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyType'></a>
### studyType `property`

##### Summary

This property holds the value of Study Type for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetClinicalStudyDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetClinicalStudyDTO.studyId')

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO'></a>
## GetStudyAuditDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for AuditTrail of a study

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO-auditTrail'></a>
### auditTrail `property`

##### Summary

This property holds the List of Audit Trail for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO-studyId'></a>
### studyId `property`

##### Summary

This property holds the value of Study ID

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO'></a>
## GetStudyDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for all elements of a study

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO-auditTrail'></a>
### auditTrail `property`

##### Summary

This property holds the Audit Trail Component of the Study

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO-clinicalStudy'></a>
### clinicalStudy `property`

##### Summary

This property holds the ClinicalStudy Component of the Study

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO'></a>
## GetStudyDesignsDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for study design sections of a study

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-investigationalInterventions'></a>
### investigationalInterventions `property`

##### Summary

This property holds the Investigational Interventions for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-plannedWorkflows'></a>
### plannedWorkflows `property`

##### Summary

This property holds the Planned Workflows for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyCells'></a>
### studyCells `property`

##### Summary

This property holds the Study Cells for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId'></a>
### studyDesignId `property`

##### Summary

This property holds the value of Study Design ID

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyPopulations'></a>
### studyPopulations `property`

##### Summary

This property holds the Study Populations for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-trialIntentType'></a>
### trialIntentType `property`

##### Summary

This property holds the value of Trial Intent Type for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-trialType'></a>
### trialType `property`

##### Summary

This property holds the value of Trial Type for specific [studyDesignId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudyDesignsDTO-studyDesignId 'TransCelerate.SDR.Core.DTO.Study.GetStudyDesignsDTO.studyDesignId')

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO'></a>
## GetStudyHistoryResponseDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for all study Id

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO-study'></a>
### study `property`

##### Summary

This property holds the Study History details

<a name='T-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO'></a>
## GetStudySectionsDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO.Study

##### Summary

This class is a DTO for GET Method for Study Level sections

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-objectives'></a>
### objectives `property`

##### Summary

This property holds the List of Study Objectives for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyDesigns'></a>
### studyDesigns `property`

##### Summary

This property holds the List of Study Designs for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyId'></a>
### studyId `property`

##### Summary

This property holds the value of Study ID

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyIndications'></a>
### studyIndications `property`

##### Summary

This property holds the List of Study Indications for specific [studyId](#P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyId 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO.studyId')

<a name='P-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-studyVersion'></a>
### studyVersion `property`

##### Summary

This property holds the value of version of Study

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters'></a>
## GroupFilters `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is used for applying group filters

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters-GetFilterValues-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},System-String-'></a>
### GetFilterValues(groups,field) `method`

##### Summary

Get filter values from the groups

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') | List of Groups which user was tagged into |
| field | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyType or study |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-GroupFilters-GetGroupFilters-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity}-'></a>
### GetGroupFilters(groups) `method`

##### Summary

This method is to return the group filter values

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') | List of Groups which user was tagged into |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper'></a>
## Helper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1

##### Summary

This class is used as a helper for different funtionalities

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForCodeSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity}-'></a>
### CheckForCodeSection(incomingCodes,existingCodes) `method`

##### Summary

Comparison between existing and incoming Code

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingCodes | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}') |  |
| existingCodes | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForDefinedProceduresSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity}-'></a>
### CheckForDefinedProceduresSection(incomingDefinedProcedures,exisitingDefinedProcedures) `method`

##### Summary

Comparison between existing and incoming Defined Procedures

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingDefinedProcedures | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}') |  |
| exisitingDefinedProcedures | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForEncounterListSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EncounterEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EncounterEntity}-'></a>
### CheckForEncounterListSection(incomingEncounters,existingEncounters) `method`

##### Summary

Comparison between existing and incoming Encounters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingEncounters | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity}') |  |
| existingEncounters | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EncounterEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForIntercurrentEventsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity}-'></a>
### CheckForIntercurrentEventsSection(incomingInterCurrentEvents,exisitingInterCurrentEvents) `method`

##### Summary

Comparison between existing and incoming Intercurrent events

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingInterCurrentEvents | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}') |  |
| exisitingInterCurrentEvents | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForInvestigationalInterventionsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### CheckForInvestigationalInterventionsSection(incomingInvestigationalInterventions,existingInvestigationalInterventions) `method`

##### Summary

Comparison between existing and incoming Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingInvestigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |
| existingInvestigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForSections-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### CheckForSections(incoming,existing) `method`

##### Summary

Comparison between existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyCellsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### CheckForStudyCellsSection(incomingStudyCells,existingStudyCells) `method`

##### Summary

Comparison between existing and incoming Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |
| existingStudyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDataCollectionSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity}-'></a>
### CheckForStudyDataCollectionSection(incomingStudyDataCollections,existingStudyDataCollections) `method`

##### Summary

Comparison between existing and incoming Study Data Collections

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDataCollections | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}') |  |
| existingStudyDataCollections | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDesignPopulationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity}-'></a>
### CheckForStudyDesignPopulationsSection(incomingStudyDesignPopulations,existingStudyDesignPopulations) `method`

##### Summary

Comparison between existing and incoming Study Design Population

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDesignPopulations | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}') |  |
| existingStudyDesignPopulations | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyDesignSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### CheckForStudyDesignSection(incomingStudyDesigns,existingStudyDesigns) `method`

##### Summary

Comparison between existing and incoming Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |
| existingStudyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyElementsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity}-'></a>
### CheckForStudyElementsSection(incomingStudyElements,existingStudyElements) `method`

##### Summary

Comparison between existing and incoming Study Elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyElements | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}') |  |
| existingStudyElements | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyEstimandSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### CheckForStudyEstimandSection(incomingEstimands,existingEstimands) `method`

##### Summary

Comparison between existing and incoming Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingEstimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |
| existingEstimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyIdentifierSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### CheckForStudyIdentifierSection(incomingStudyIdentifiers,existingStudyIdentifiers) `method`

##### Summary

Comparison between existing and incoming Study Identifiers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |
| existingStudyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyIndicationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### CheckForStudyIndicationsSection(incomingIndications,exisitingIndications) `method`

##### Summary

Comparison between existing and incoming Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingIndications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |
| exisitingIndications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyObjectivesEndpointsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity}-'></a>
### CheckForStudyObjectivesEndpointsSection(incomingEndpoints,existingEndpoints) `method`

##### Summary

Comparison between existing and incoming Study Objective Endpoints

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingEndpoints | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}') |  |
| existingEndpoints | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyObjectivesSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### CheckForStudyObjectivesSection(incomingObjectives,existingObjectives) `method`

##### Summary

Comparison between existing and incoming Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingObjectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |
| existingObjectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyProtocolSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### CheckForStudyProtocolSection(incomingStudyProtocolVersions,existingStudyProtocolVersions) `method`

##### Summary

Comparison between existing and incoming Study ProtocolVersions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |
| existingStudyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyWorkflowItemsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity}-'></a>
### CheckForStudyWorkflowItemsSection(incomingWorkflowItems,existingWorkflowItems) `method`

##### Summary

Comparison between existing and incoming Study WorkFlow Items

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingWorkflowItems | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}') |  |
| existingWorkflowItems | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-CheckForStudyWorkflowSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### CheckForStudyWorkflowSection(incomingWorkflows,existingWorkflows) `method`

##### Summary

Comparison between existing and incoming Study WorkFlows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingWorkflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |
| existingWorkflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### GenerateIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Generate uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### GenerateIdForStudyCells(studyCells) `method`

##### Summary

Generate uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### GenerateIdForStudyDesign(studyDesigns) `method`

##### Summary

Generate uuid for Study StudyDesigns

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### GenerateIdForStudyEstimand(estimands) `method`

##### Summary

Generate uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### GenerateIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Generate uuid for Study Identifiers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### GenerateIdForStudyIndications(indications) `method`

##### Summary

Generate uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### GenerateIdForStudyObjectives(objectives) `method`

##### Summary

Generate uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### GenerateIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Generate uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GenerateIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### GenerateIdForStudyWorkflow(workflows) `method`

##### Summary

Generate uuid for Study Workflows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GeneratedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### GeneratedSectionId(study) `method`

##### Summary

Generate uuid for Each section of study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') | Study Entity |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-GetAuditTrail-System-String-'></a>
### GetAuditTrail(user) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemoveIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### RemoveIdForStudyWorkflow(workflows) `method`

##### Summary

Remove uuid for Study Workflows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-Helper-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

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

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper'></a>
## IHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForCodeSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-CodeEntity}-'></a>
### CheckForCodeSection(incomingCodes,existingCodes) `method`

##### Summary

Comparison between existing and incoming Code

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingCodes | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}') |  |
| existingCodes | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.CodeEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForDefinedProceduresSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-DefinedProcedureEntity}-'></a>
### CheckForDefinedProceduresSection(incomingDefinedProcedures,exisitingDefinedProcedures) `method`

##### Summary

Comparison between existing and incoming Defined Procedures

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingDefinedProcedures | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}') |  |
| exisitingDefinedProcedures | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.DefinedProcedureEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForIntercurrentEventsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InterCurrentEventEntity}-'></a>
### CheckForIntercurrentEventsSection(incomingInterCurrentEvents,exisitingInterCurrentEvents) `method`

##### Summary

Comparison between existing and incoming Intercurrent events

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingInterCurrentEvents | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}') |  |
| exisitingInterCurrentEvents | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InterCurrentEventEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForInvestigationalInterventionsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### CheckForInvestigationalInterventionsSection(incomingInvestigationalInterventions,existingInvestigationalInterventions) `method`

##### Summary

Comparison between existing and incoming Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingInvestigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |
| existingInvestigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForSections-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### CheckForSections(incoming,existing) `method`

##### Summary

Comparison between existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyCellsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### CheckForStudyCellsSection(incomingStudyCells,existingStudyCells) `method`

##### Summary

Comparison between existing and incoming Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |
| existingStudyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDataCollectionSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDataCollectionEntity}-'></a>
### CheckForStudyDataCollectionSection(incomingStudyDataCollections,existingStudyDataCollections) `method`

##### Summary

Comparison between existing and incoming Study Data Collections

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDataCollections | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}') |  |
| existingStudyDataCollections | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDataCollectionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDesignPopulationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignPopulationEntity}-'></a>
### CheckForStudyDesignPopulationsSection(incomingStudyDesignPopulations,existingStudyDesignPopulations) `method`

##### Summary

Comparison between existing and incoming Study Design Population

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDesignPopulations | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}') |  |
| existingStudyDesignPopulations | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignPopulationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyDesignSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### CheckForStudyDesignSection(incomingStudyDesigns,existingStudyDesigns) `method`

##### Summary

Comparison between existing and incoming Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |
| existingStudyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyElementsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyElementEntity}-'></a>
### CheckForStudyElementsSection(incomingStudyElements,existingStudyElements) `method`

##### Summary

Comparison between existing and incoming Study Elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyElements | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}') |  |
| existingStudyElements | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyElementEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyEstimandSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### CheckForStudyEstimandSection(incomingEstimands,existingEstimands) `method`

##### Summary

Comparison between existing and incoming Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingEstimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |
| existingEstimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyIdentifierSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### CheckForStudyIdentifierSection(incomingStudyIdentifiers,existingStudyIdentifiers) `method`

##### Summary

Comparison between existing and incoming Study Identifiers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |
| existingStudyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyIndicationsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### CheckForStudyIndicationsSection(incomingIndications,exisitingIndications) `method`

##### Summary

Comparison between existing and incoming Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingIndications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |
| exisitingIndications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyObjectivesEndpointsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EndpointEntity}-'></a>
### CheckForStudyObjectivesEndpointsSection(incomingEndpoints,existingEndpoints) `method`

##### Summary

Comparison between existing and incoming Study Objective Endpoints

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingEndpoints | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}') |  |
| existingEndpoints | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EndpointEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyObjectivesSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### CheckForStudyObjectivesSection(incomingObjectives,existingObjectives) `method`

##### Summary

Comparison between existing and incoming Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingObjectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |
| existingObjectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyProtocolSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### CheckForStudyProtocolSection(incomingStudyProtocolVersions,existingStudyProtocolVersions) `method`

##### Summary

Comparison between existing and incoming Study ProtocolVersions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingStudyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |
| existingStudyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyWorkflowItemsSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkFlowItemEntity}-'></a>
### CheckForStudyWorkflowItemsSection(incomingWorkflowItems,existingWorkflowItems) `method`

##### Summary

Comparison between existing and incoming Study WorkFlow Items

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingWorkflowItems | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}') |  |
| existingWorkflowItems | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkFlowItemEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-CheckForStudyWorkflowSection-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### CheckForStudyWorkflowSection(incomingWorkflows,existingWorkflows) `method`

##### Summary

Comparison between existing and incoming Study WorkFlows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incomingWorkflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |
| existingWorkflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### GenerateIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Generate uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### GenerateIdForStudyCells(studyCells) `method`

##### Summary

Generate uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### GenerateIdForStudyDesign(studyDesigns) `method`

##### Summary

Generate uuid for Study StudyDesigns

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### GenerateIdForStudyEstimand(estimands) `method`

##### Summary

Generate uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### GenerateIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Generate uuid for Study Identifiers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### GenerateIdForStudyIndications(indications) `method`

##### Summary

Generate uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### GenerateIdForStudyObjectives(objectives) `method`

##### Summary

Generate uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### GenerateIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Generate uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GenerateIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### GenerateIdForStudyWorkflow(workflows) `method`

##### Summary

Generate uuid for Study Workflows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GeneratedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### GeneratedSectionId(study) `method`

##### Summary

Generate uuid for Each section of study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') | Study Entity |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-GetAuditTrail-System-String-'></a>
### GetAuditTrail(user) `method`

##### Summary

Get Audit Trail fields for the POST Api

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-IsSameStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### IsSameStudy(incoming,existing) `method`

##### Summary

Compare Full Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |
| existing | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

Deep compare of existing and incoming study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForInvestigationalInterventions-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-InvestigationalInterventionEntity}-'></a>
### RemoveIdForInvestigationalInterventions(investigationalInterventions) `method`

##### Summary

Remove uuid for Study Investigational Interventions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| investigationalInterventions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.InvestigationalInterventionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyCells-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyCellEntity}-'></a>
### RemoveIdForStudyCells(studyCells) `method`

##### Summary

Remove uuid for Study Cells

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCells | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyCellEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyDesign-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyDesignEntity}-'></a>
### RemoveIdForStudyDesign(studyDesigns) `method`

##### Summary

Remove uuid for Study Designs

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesigns | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyDesignEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyEstimand-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-EstimandEntity}-'></a>
### RemoveIdForStudyEstimand(estimands) `method`

##### Summary

Remove uuid for Study Estimands

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| estimands | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.EstimandEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyIdentifier-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyIdentifierEntity}-'></a>
### RemoveIdForStudyIdentifier(studyIdentifiers) `method`

##### Summary

Remove uuid for Study Identifier

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyIdentifiers | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyIdentifierEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyIndications-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-IndicationEntity}-'></a>
### RemoveIdForStudyIndications(indications) `method`

##### Summary

Remove uuid for Study Indications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| indications | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.IndicationEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyObjectives-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-ObjectiveEntity}-'></a>
### RemoveIdForStudyObjectives(objectives) `method`

##### Summary

Remove uuid for Study Objectives

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objectives | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.ObjectiveEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyProtocol-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-StudyProtocolVersionEntity}-'></a>
### RemoveIdForStudyProtocol(studyProtocolVersions) `method`

##### Summary

Remove uuid for Study Protocol Versions

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyProtocolVersions | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.StudyProtocolVersionEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemoveIdForStudyWorkflow-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-WorkflowEntity}-'></a>
### RemoveIdForStudyWorkflow(workflows) `method`

##### Summary

Remove uuid for Study Workflows

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workflows | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.WorkflowEntity}') |  |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-HelpersV1-IHelper-RemovedSectionId-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### RemovedSectionId(study) `method`

##### Summary

Remode uuid for Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') |  |

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

<a name='T-TransCelerate-SDR-Core-DTO-PostStudyDTO'></a>
## PostStudyDTO `type`

##### Namespace

TransCelerate.SDR.Core.DTO

##### Summary

This class is a DTO for POST Method for a study

<a name='P-TransCelerate-SDR-Core-DTO-PostStudyDTO-auditTrail'></a>
### auditTrail `property`

##### Summary

This property holds the Audit Trail Component of the Study for POST Method

<a name='P-TransCelerate-SDR-Core-DTO-PostStudyDTO-clinicalStudy'></a>
### clinicalStudy `property`

##### Summary

This property holds the ClinicalStudy Component of the Study for POST Method

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck'></a>
## PostStudyElementsCheck `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-ActivitySectionCheck-TransCelerate-SDR-Core-Entities-Study-ActivityEntity,TransCelerate-SDR-Core-Entities-Study-ActivityEntity-'></a>
### ActivitySectionCheck(existingActivityEntity,incomingActivityEntity) `method`

##### Summary

Check for Activity section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingActivityEntity | [TransCelerate.SDR.Core.Entities.Study.ActivityEntity](#T-TransCelerate-SDR-Core-Entities-Study-ActivityEntity 'TransCelerate.SDR.Core.Entities.Study.ActivityEntity') | Existing Activity |
| incomingActivityEntity | [TransCelerate.SDR.Core.Entities.Study.ActivityEntity](#T-TransCelerate-SDR-Core-Entities-Study-ActivityEntity 'TransCelerate.SDR.Core.Entities.Study.ActivityEntity') | Incoming Activity |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-EncounterSectionCheck-TransCelerate-SDR-Core-Entities-Study-EncounterEntity,TransCelerate-SDR-Core-Entities-Study-EncounterEntity-'></a>
### EncounterSectionCheck(existingEncounterEntity,incomingEncounterEntity) `method`

##### Summary

Check for Encounter section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingEncounterEntity | [TransCelerate.SDR.Core.Entities.Study.EncounterEntity](#T-TransCelerate-SDR-Core-Entities-Study-EncounterEntity 'TransCelerate.SDR.Core.Entities.Study.EncounterEntity') | Existing Encounter |
| incomingEncounterEntity | [TransCelerate.SDR.Core.Entities.Study.EncounterEntity](#T-TransCelerate-SDR-Core-Entities-Study-EncounterEntity 'TransCelerate.SDR.Core.Entities.Study.EncounterEntity') | Incoming Encounter |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-EpochSectionCheck-TransCelerate-SDR-Core-Entities-Study-EpochEntity,TransCelerate-SDR-Core-Entities-Study-EpochEntity-'></a>
### EpochSectionCheck(existingEpochEntity,incomingEpochEntity) `method`

##### Summary

Check for Epoch section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingEpochEntity | [TransCelerate.SDR.Core.Entities.Study.EpochEntity](#T-TransCelerate-SDR-Core-Entities-Study-EpochEntity 'TransCelerate.SDR.Core.Entities.Study.EpochEntity') | Existing Epoch |
| incomingEpochEntity | [TransCelerate.SDR.Core.Entities.Study.EpochEntity](#T-TransCelerate-SDR-Core-Entities-Study-EpochEntity 'TransCelerate.SDR.Core.Entities.Study.EpochEntity') | Incoming Epoch |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-InvestigationalInvestigationSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-InvestigationalInterventionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-InvestigationalInterventionEntity}-'></a>
### InvestigationalInvestigationSectionCheck(existingStudyDesign,existingInvestigationalInterventionEntities,incomingInvestigationalInterventionEntities) `method`

##### Summary

Check for Investigational Intervention section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyDesign | [TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') | Existing Study Design |
| existingInvestigationalInterventionEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity}') | Existing InvestigationalIntervention |
| incomingInvestigationalInterventionEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.InvestigationalInterventionEntity}') | Incoming InvestigationalIntervention |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-ItemSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity}-'></a>
### ItemSectionCheck(existingItemEntities,incomingItemEntities) `method`

##### Summary

Check for Items section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingItemEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}') | Existing Items |
| incomingItemEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}') | Incoming Items |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-JsonObjectCheck-System-Object,System-Object-'></a>
### JsonObjectCheck(incoming,existing) `method`

##### Summary

This method will check whether two Json objects are same or not

##### Returns

`true` If incoming and existing study entities are identical

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object of Incoming study defenitions |
| existing | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Object of Existing study defenitions |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-MatrixSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-MatrixEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-MatrixEntity}-'></a>
### MatrixSectionCheck(existingMatrixEntities,incomingMatrixEntities) `method`

##### Summary

Check for Matrix section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingMatrixEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity}') | Existing Matrix |
| incomingMatrixEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.MatrixEntity}') | Incoming Matrix |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-PointInTimeSectionCheck-TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity,TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity-'></a>
### PointInTimeSectionCheck(existingPointInTime,incomingPointInTime) `method`

##### Summary

Check for PointInTime section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingPointInTime | [TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity](#T-TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity 'TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity') | Existing PointInTime |
| incomingPointInTime | [TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity](#T-TransCelerate-SDR-Core-Entities-Study-PointInTimeEntity 'TransCelerate.SDR.Core.Entities.Study.PointInTimeEntity') | Incoming PointInTime |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-RemoveId-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### RemoveId(studyEntity) `method`

##### Summary

This method will remove all the id fields in the study

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') after removing all ID fields

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyEntity | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study defenitions for which Id fields must be removed |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-RuleSectionCheck-TransCelerate-SDR-Core-Entities-Study-RuleEntity,TransCelerate-SDR-Core-Entities-Study-RuleEntity-'></a>
### RuleSectionCheck(existingRuleEntity,incomingRuleEntity) `method`

##### Summary

Check for Rule section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingRuleEntity | [TransCelerate.SDR.Core.Entities.Study.RuleEntity](#T-TransCelerate-SDR-Core-Entities-Study-RuleEntity 'TransCelerate.SDR.Core.Entities.Study.RuleEntity') | Existing Rule |
| incomingRuleEntity | [TransCelerate.SDR.Core.Entities.Study.RuleEntity](#T-TransCelerate-SDR-Core-Entities-Study-RuleEntity 'TransCelerate.SDR.Core.Entities.Study.RuleEntity') | Incoming Rule |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-SectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### SectionCheck(incoming,existing) `method`

##### Summary

Section Level Study comparison for sectional POST

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') after checking the section for Posting the data to the database

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Incoming Study Defenitions |
| existing | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Existing Study Defenitions |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyArmSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyArmEntity,TransCelerate-SDR-Core-Entities-Study-StudyArmEntity-'></a>
### StudyArmSectionCheck(existingStudyArm,incomingStudyArm) `method`

##### Summary

Check for StudyArm section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyArm | [TransCelerate.SDR.Core.Entities.Study.StudyArmEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyArmEntity 'TransCelerate.SDR.Core.Entities.Study.StudyArmEntity') | Existing StudyArm |
| incomingStudyArm | [TransCelerate.SDR.Core.Entities.Study.StudyArmEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyArmEntity 'TransCelerate.SDR.Core.Entities.Study.StudyArmEntity') | Incoming StudyArm |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyCellsSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyCellEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyCellEntity}-'></a>
### StudyCellsSectionCheck(existingStudyDesign,existingStudyCellsEntities,incomingStudyCellsEntities) `method`

##### Summary

Check for StudyCells section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyDesign | [TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') | Existing Study Design |
| existingStudyCellsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity}') | Existing Study Cells |
| incomingStudyCellsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyCellEntity}') | Incoming Study Cells |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyComparison-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### StudyComparison(incoming,existing) `method`

##### Summary

This method is used to check whether the incoming study and existing study are same

##### Returns

`true` If incoming and existing study entities are identical

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| incoming | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Incoming study defenitions |
| existing | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Existing study defenitions |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyDataCollectionSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDataCollectionEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDataCollectionEntity}-'></a>
### StudyDataCollectionSectionCheck(existingStudyDataCollectionEntities,incomingStudyDataCollectionEntities) `method`

##### Summary

Check for StudyDataCollection section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyDataCollectionEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity}') | Existing StudyDataCollection |
| incomingStudyDataCollectionEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDataCollectionEntity}') | Incoming StudyDataCollection |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyDesignSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity}-'></a>
### StudyDesignSectionCheck(existing,existingStudyDesignEntities,incomingStudyDesignEntities) `method`

##### Summary

Check for Study Design section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existing | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Existing Study Defenitions |
| existingStudyDesignEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity}') | Existing Study Designs |
| incomingStudyDesignEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity}') | Existing Study Designs |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyElementsSectionCheck-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyElementEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyElementEntity}-'></a>
### StudyElementsSectionCheck(existingStudyElementsEntities,incomingStudyElementsEntities) `method`

##### Summary

Check for StudyElements section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyElementsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity}') | Existing StudyElement |
| incomingStudyElementsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyElementEntity}') | Incoming StudyElement |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyEpochSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity,TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity-'></a>
### StudyEpochSectionCheck(existingStudyEpoch,incomingStudyEpoch) `method`

##### Summary

Check for StudyEpoch section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyEpoch | [TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity') | Existing StudyEpoch |
| incomingStudyEpoch | [TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEpochEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEpochEntity') | Incoming StudyEpoch |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyIndicationSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyIndicationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyIndicationEntity}-'></a>
### StudyIndicationSectionCheck(existing,existingStudyIndicationEntities,incomingStudyIndicationEntities) `method`

##### Summary

Check for Study Indication section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existing | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Existing Study Defenitions |
| existingStudyIndicationEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity}') | Existing Study Indications |
| incomingStudyIndicationEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyIndicationEntity}') | Incoming Study Indications |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyObjectivesSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity}-'></a>
### StudyObjectivesSectionCheck(existing,existingStudyObjectivesEntities,incomingStudyObjectivesEntities) `method`

##### Summary

Check for Study Objectives section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existing | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Existing Study Defenitions |
| existingStudyObjectivesEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity}') | Existing Study Objectives |
| incomingStudyObjectivesEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity}') | Incoming Study Objectives |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyPlannedWorkFlowSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity}-'></a>
### StudyPlannedWorkFlowSectionCheck(existingStudyDesign,existingPlannedWorkFlowsEntities,incomingPlannedWorkFlowsEntities) `method`

##### Summary

Check for Study Planned WorkFlow section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyDesign | [TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') | Existing Study Design |
| existingPlannedWorkFlowsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity}') | Existing PlannedWorkflows |
| incomingPlannedWorkFlowsEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity}') | Incoming PlannedWorkflows |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-StudyPopulationsSectionCheck-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyPopulationEntity},System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyPopulationEntity}-'></a>
### StudyPopulationsSectionCheck(existingStudyDesign,existingStudyPopulationEntities,incomingStudyPopulationEntities) `method`

##### Summary

Check for Study Population section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingStudyDesign | [TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') | Existing Study Design |
| existingStudyPopulationEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity}') | Existing Study Populations |
| incomingStudyPopulationEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyPopulationEntity}') | Incoming Study Populations |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PostStudyElementsCheck-WorkFlowItemMatrixSectionCheck-TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity,TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity-'></a>
### WorkFlowItemMatrixSectionCheck(existingWorkFlowItemMatrixEntity,incomingWorkFlowItemMatrixEntity) `method`

##### Summary

Check for WorkFlowItemMatrix section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| existingWorkFlowItemMatrixEntity | [TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity](#T-TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity 'TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity') | Existing WorkFlowItemMatrix |
| incomingWorkFlowItemMatrixEntity | [TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity](#T-TransCelerate-SDR-Core-Entities-Study-WorkFlowItemMatrixEntity 'TransCelerate.SDR.Core.Entities.Study.WorkFlowItemMatrixEntity') | Incoming WorkFlowItemMatrix |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper'></a>
## PreviousItemNextItemHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is to get Previous Items and Next Items for a Study

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper-GetPreviousNextItems-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-ItemEntity}-'></a>
### GetPreviousNextItems(itemEntities) `method`

##### Summary

Generate the Previous Items and Next Items

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') after creating previuosItems and NextItems Array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| itemEntities | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.ItemEntity}') | Item List for which Previous and Next Items need to be created |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-PreviousItemNextItemHelper-PreviousItemsNextItemsWraper-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### PreviousItemsNextItemsWraper(studyEntity) `method`

##### Summary

Get the ItemList from the Study

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') after creating previuosItems and NextItems Array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyEntity | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study Entity for which Previous and Next Items need to be created |

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections'></a>
## RemoveStudySections `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is for removing sections from the study for sectional response

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-PostResponseRemoveSections-TransCelerate-SDR-Core-DTO-PostStudyDTO-'></a>
### PostResponseRemoveSections(studyEntity) `method`

##### Summary

This method is for formatting study definitions to POST request format

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') After formating the JSON to the POST request format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyEntity | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for formatting the response |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-RemoveSections-System-String[],TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-'></a>
### RemoveSections(sections,getStudySectionsDTO) `method`

##### Summary

This method is for removing study level sections

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') After removing sections which are not in the sections array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study Sections array |
| getStudySectionsDTO | [TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO') | Study defenitions from Database |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-RemoveStudySections-RemoveSectionsForStudyDesign-System-String[],TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO-'></a>
### RemoveSectionsForStudyDesign(sections,getStudySectionsDTO) `method`

##### Summary

This method is for removing study design level sections

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') After removing sections which are not in the sections array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study Design Sections array |
| getStudySectionsDTO | [TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudySectionsDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudySectionsDTO') | Study defenitions from Database |

<a name='T-TransCelerate-SDR-Core-Utilities-Common-Route'></a>
## Route `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Common

##### Summary

This class holds list of routes for all the endpoints

<a name='T-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleResponseDto'></a>
## SearchTitleResponseDto `type`

##### Namespace

TransCelerate.SDR.Core.DTO.StudyV1

##### Summary

This class is a DTO for response of Search StudyTitle Endpoint

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator'></a>
## SectionIdGenerator `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is for generating Id for each Sections

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-GenerateSectionId-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### GenerateSectionId(studyEntity) `method`

##### Summary

Generate Id for all the sections

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') after generating ID for all Sections

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyEntity | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study for which Id's to be generated |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyCellsIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyCellEntity-'></a>
### StudyCellsIdGenerator(studyCellEntity) `method`

##### Summary

Generate Id for Study Cells

##### Returns

A [StudyCellEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyCellEntity 'TransCelerate.SDR.Core.Entities.Study.StudyCellEntity') after generating ID for StudyCell Section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyCellEntity | [TransCelerate.SDR.Core.Entities.Study.StudyCellEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyCellEntity 'TransCelerate.SDR.Core.Entities.Study.StudyCellEntity') | Study Cells for which Id's to be generated |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyDesignIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity-'></a>
### StudyDesignIdGenerator(studyDesignEntity) `method`

##### Summary

Generate Id for Study Designs

##### Returns

A [StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') after generating ID for StudyDesign Section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDesignEntity | [TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity 'TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity') | Study Design for which Id's to be generated |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyObjectivesIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity-'></a>
### StudyObjectivesIdGenerator(studyObjectivesEntity) `method`

##### Summary

Generate Id for Study Objectives

##### Returns

A [StudyObjectiveEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity 'TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity') after generating ID for StudyObjective Section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyObjectivesEntity | [TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity 'TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity') | Study Objectives for which Id's to be generated |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyPlannedWorkFlowIdGenerator-TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity-'></a>
### StudyPlannedWorkFlowIdGenerator(plannedWorkFlowEntity) `method`

##### Summary

Generate Id for Study PlannedWorkFlows

##### Returns

A [PlannedWorkFlowEntity](#T-TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity 'TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity') after generating ID for PlannedWorkFlow Section

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| plannedWorkFlowEntity | [TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity](#T-TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity 'TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity') | Planned workflow for which Id's to be generated |

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

<a name='T-TransCelerate-SDR-Core-DTO-StudyV1-StudyHistoryResponseDto'></a>
## StudyHistoryResponseDto `type`

##### Namespace

TransCelerate.SDR.Core.DTO.StudyV1

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

<a name='T-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper'></a>
## UserGroupSortingHelper `type`

##### Namespace

TransCelerate.SDR.Core.Utilities.Helpers

##### Summary

This class is a helper for sorting groups and users

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper-OrderGroups-System-Collections-Generic-IEnumerable{TransCelerate-SDR-Core-Entities-UserGroups-GroupDetailsEntity},TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### OrderGroups(groupDetails,userGroupsQueryParameters) `method`

##### Summary

Sorting a Group List

##### Returns

A [IOrderedEnumerable\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IOrderedEnumerable`1 'System.Linq.IOrderedEnumerable`1') after sorting the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDetails | [System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.Entities.UserGroups.GroupDetailsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.Entities.UserGroups.GroupDetailsEntity}') | Group List which needed to be sorted |
| userGroupsQueryParameters | [TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters](#T-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters 'TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters') | parameters for sorting |

<a name='M-TransCelerate-SDR-Core-Utilities-Helpers-UserGroupSortingHelper-OrderUsers-System-Collections-Generic-IEnumerable{TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO},TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### OrderUsers(users,userGroupsQueryParameters) `method`

##### Summary

Sorting a User List

##### Returns

A [IOrderedEnumerable\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IOrderedEnumerable`1 'System.Linq.IOrderedEnumerable`1') after sorting the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| users | [System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO}') | User List which needed to be sorted |
| userGroupsQueryParameters | [TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters](#T-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters 'TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters') | parameters for sorting |

<a name='T-TransCelerate-SDR-Core-ErrorModels-ValidationErrorModel'></a>
## ValidationErrorModel `type`

##### Namespace

TransCelerate.SDR.Core.ErrorModels

##### Summary

This class is a Model for validation errors
