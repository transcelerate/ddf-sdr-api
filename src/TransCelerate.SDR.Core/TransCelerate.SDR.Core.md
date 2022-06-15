<a name='assembly'></a>
# TransCelerate.SDR.Core

## Contents

- [ActionFilter](#T-TransCelerate-SDR-Core-Filters-ActionFilter 'TransCelerate.SDR.Core.Filters.ActionFilter')
- [AllowAnonymousFilter](#T-TransCelerate-SDR-Core-Filters-AllowAnonymousFilter 'TransCelerate.SDR.Core.Filters.AllowAnonymousFilter')
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
- [HttpContextResponseHelper](#T-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper')
  - [Response(context,response)](#M-TransCelerate-SDR-Core-Utilities-Helpers-HttpContextResponseHelper-Response-Microsoft-AspNetCore-Http-HttpContext,System-String- 'TransCelerate.SDR.Core.Utilities.Helpers.HttpContextResponseHelper.Response(Microsoft.AspNetCore.Http.HttpContext,System.String)')
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
- [SectionIdGenerator](#T-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator')
  - [GenerateSectionId(studyEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-GenerateSectionId-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.GenerateSectionId(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [StudyCellsIdGenerator(studyCellEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyCellsIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyCellEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyCellsIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyCellEntity)')
  - [StudyDesignIdGenerator(studyDesignEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyDesignIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyDesignEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyDesignIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyDesignEntity)')
  - [StudyObjectivesIdGenerator(studyObjectivesEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyObjectivesIdGenerator-TransCelerate-SDR-Core-Entities-Study-StudyObjectiveEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyObjectivesIdGenerator(TransCelerate.SDR.Core.Entities.Study.StudyObjectiveEntity)')
  - [StudyPlannedWorkFlowIdGenerator(plannedWorkFlowEntity)](#M-TransCelerate-SDR-Core-Utilities-Helpers-SectionIdGenerator-StudyPlannedWorkFlowIdGenerator-TransCelerate-SDR-Core-Entities-Study-PlannedWorkFlowEntity- 'TransCelerate.SDR.Core.Utilities.Helpers.SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(TransCelerate.SDR.Core.Entities.Study.PlannedWorkFlowEntity)')
- [SortOrder](#T-TransCelerate-SDR-Core-Utilities-SortOrder 'TransCelerate.SDR.Core.Utilities.SortOrder')
- [StartupLib](#T-TransCelerate-SDR-Core-AppSettings-StartupLib 'TransCelerate.SDR.Core.AppSettings.StartupLib')
  - [SetConstants(config)](#M-TransCelerate-SDR-Core-AppSettings-StartupLib-SetConstants-Microsoft-Extensions-Configuration-IConfiguration- 'TransCelerate.SDR.Core.AppSettings.StartupLib.SetConstants(Microsoft.Extensions.Configuration.IConfiguration)')
- [StudyDesignSections](#T-TransCelerate-SDR-Core-Utilities-StudyDesignSections 'TransCelerate.SDR.Core.Utilities.StudyDesignSections')
- [StudySectionTypes](#T-TransCelerate-SDR-Core-Utilities-StudySectionTypes 'TransCelerate.SDR.Core.Utilities.StudySectionTypes')
- [StudySections](#T-TransCelerate-SDR-Core-Utilities-StudySections 'TransCelerate.SDR.Core.Utilities.StudySections')
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
