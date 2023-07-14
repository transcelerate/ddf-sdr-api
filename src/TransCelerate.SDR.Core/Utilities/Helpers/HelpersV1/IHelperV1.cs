using System.Collections.Generic;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1
{
    public interface IHelperV1
    {
        #region Check for each sections
        /// <summary>
        /// Comparison between existing and incoming Code
        /// </summary>
        /// <param name="incomingCodes"></param>
        /// <param name="existingCodes"></param>
        /// <returns></returns>
        List<CodeEntity> CheckForCodeSection(List<CodeEntity> incomingCodes, List<CodeEntity> existingCodes);
        /// <summary>
        /// Comparison between existing and incoming Defined Procedures
        /// </summary>
        /// <param name="incomingDefinedProcedures"></param>
        /// <param name="exisitingDefinedProcedures"></param>
        /// <returns></returns>
        List<DefinedProcedureEntity> CheckForDefinedProceduresSection(List<DefinedProcedureEntity> incomingDefinedProcedures, List<DefinedProcedureEntity> exisitingDefinedProcedures);
        /// <summary>
        /// Comparison between existing and incoming Intercurrent events
        /// </summary>
        /// <param name="incomingInterCurrentEvents"></param>
        /// <param name="exisitingInterCurrentEvents"></param>
        /// <returns></returns>
        List<InterCurrentEventEntity> CheckForIntercurrentEventsSection(List<InterCurrentEventEntity> incomingInterCurrentEvents, List<InterCurrentEventEntity> exisitingInterCurrentEvents);
        /// <summary>
        /// Comparison between existing and incoming Study Investigational Interventions
        /// </summary>
        /// <param name="incomingInvestigationalInterventions"></param>
        /// <param name="existingInvestigationalInterventions"></param>
        /// <returns></returns>
        List<InvestigationalInterventionEntity> CheckForInvestigationalInterventionsSection(List<InvestigationalInterventionEntity> incomingInvestigationalInterventions, List<InvestigationalInterventionEntity> existingInvestigationalInterventions);
        /// <summary>
        /// Comparison between existing and incoming study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        StudyDefinitionsEntity CheckForSections(StudyDefinitionsEntity incoming, StudyDefinitionsEntity existing);
        /// <summary>
        /// Comparison between existing and incoming Study Cells
        /// </summary>
        /// <param name="incomingStudyCells"></param>
        /// <param name="existingStudyCells"></param>
        /// <returns></returns>
        List<StudyCellEntity> CheckForStudyCellsSection(List<StudyCellEntity> incomingStudyCells, List<StudyCellEntity> existingStudyCells);
        /// <summary>
        /// Comparison between existing and incoming Study Data Collections
        /// </summary>
        /// <param name="incomingStudyDataCollections"></param>
        /// <param name="existingStudyDataCollections"></param>
        /// <returns></returns>
        List<StudyDataCollectionEntity> CheckForStudyDataCollectionSection(List<StudyDataCollectionEntity> incomingStudyDataCollections, List<StudyDataCollectionEntity> existingStudyDataCollections);
        /// <summary>
        /// Comparison between existing and incoming Study Design Population
        /// </summary>
        /// <param name="incomingStudyDesignPopulations"></param>
        /// <param name="existingStudyDesignPopulations"></param>
        /// <returns></returns>
        List<StudyDesignPopulationEntity> CheckForStudyDesignPopulationsSection(List<StudyDesignPopulationEntity> incomingStudyDesignPopulations, List<StudyDesignPopulationEntity> existingStudyDesignPopulations);
        /// <summary>
        /// Comparison between existing and incoming Study Designs
        /// </summary>
        /// <param name="incomingStudyDesigns"></param>
        /// <param name="existingStudyDesigns"></param>
        /// <returns></returns>
        List<StudyDesignEntity> CheckForStudyDesignSection(List<StudyDesignEntity> incomingStudyDesigns, List<StudyDesignEntity> existingStudyDesigns);
        /// <summary>
        /// Comparison between existing and incoming Study Elements
        /// </summary>
        /// <param name="incomingStudyElements"></param>
        /// <param name="existingStudyElements"></param>
        /// <returns></returns>
        List<StudyElementEntity> CheckForStudyElementsSection(List<StudyElementEntity> incomingStudyElements, List<StudyElementEntity> existingStudyElements);
        /// <summary>
        /// Comparison between existing and incoming Study Estimands
        /// </summary>
        /// <param name="incomingEstimands"></param>
        /// <param name="existingEstimands"></param>
        /// <returns></returns>
        List<EstimandEntity> CheckForStudyEstimandSection(List<EstimandEntity> incomingEstimands, List<EstimandEntity> existingEstimands);
        /// <summary>
        /// Comparison between existing and incoming Study Identifiers
        /// </summary>
        /// <param name="incomingStudyIdentifiers"></param>
        /// <param name="existingStudyIdentifiers"></param>
        /// <returns></returns>
        List<StudyIdentifierEntity> CheckForStudyIdentifierSection(List<StudyIdentifierEntity> incomingStudyIdentifiers, List<StudyIdentifierEntity> existingStudyIdentifiers);
        /// <summary>
        /// Comparison between existing and incoming Study Indications
        /// </summary>
        /// <param name="incomingIndications"></param>
        /// <param name="exisitingIndications"></param>
        /// <returns></returns>
        List<IndicationEntity> CheckForStudyIndicationsSection(List<IndicationEntity> incomingIndications, List<IndicationEntity> exisitingIndications);
        /// <summary>
        /// Comparison between existing and incoming Study Objective Endpoints
        /// </summary>
        /// <param name="incomingEndpoints"></param>
        /// <param name="existingEndpoints"></param>
        /// <returns></returns>
        List<EndpointEntity> CheckForStudyObjectivesEndpointsSection(List<EndpointEntity> incomingEndpoints, List<EndpointEntity> existingEndpoints);
        /// <summary>
        /// Comparison between existing and incoming Study Objectives
        /// </summary>
        /// <param name="incomingObjectives"></param>
        /// <param name="existingObjectives"></param>
        /// <returns></returns>
        List<ObjectiveEntity> CheckForStudyObjectivesSection(List<ObjectiveEntity> incomingObjectives, List<ObjectiveEntity> existingObjectives);
        /// <summary>
        /// Comparison between existing and incoming Study ProtocolVersions
        /// </summary>
        /// <param name="incomingStudyProtocolVersions"></param>
        /// <param name="existingStudyProtocolVersions"></param>
        /// <returns></returns>
        List<StudyProtocolVersionEntity> CheckForStudyProtocolSection(List<StudyProtocolVersionEntity> incomingStudyProtocolVersions, List<StudyProtocolVersionEntity> existingStudyProtocolVersions);
        /// <summary>
        /// Comparison between existing and incoming Study WorkFlow Items
        /// </summary>
        /// <param name="incomingWorkflowItems"></param>
        /// <param name="existingWorkflowItems"></param>
        /// <returns></returns>
        List<WorkFlowItemEntity> CheckForStudyWorkflowItemsSection(List<WorkFlowItemEntity> incomingWorkflowItems, List<WorkFlowItemEntity> existingWorkflowItems);
        /// <summary>
        /// Comparison between existing and incoming Study WorkFlows
        /// </summary>
        /// <param name="incomingWorkflows"></param>
        /// <param name="existingWorkflows"></param>
        /// <returns></returns>
        List<WorkflowEntity> CheckForStudyWorkflowSection(List<WorkflowEntity> incomingWorkflows, List<WorkflowEntity> existingWorkflows);
        #endregion

        #region Generate Id for each sections
        /// <summary>
        /// Generate uuid for Each section of study
        /// </summary>
        /// <param name="study">Study Entity</param>
        /// <returns></returns>
        StudyDefinitionsEntity GeneratedSectionId(StudyDefinitionsEntity study);
        /// <summary>
        /// Generate uuid for Study Investigational Interventions
        /// </summary>
        /// <param name="investigationalInterventions"></param>
        /// <returns></returns>
        List<InvestigationalInterventionEntity> GenerateIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions);
        /// <summary>
        /// Generate uuid for Study Cells
        /// </summary>
        /// <param name="studyCells"></param>
        /// <returns></returns>
        List<StudyCellEntity> GenerateIdForStudyCells(List<StudyCellEntity> studyCells);
        /// <summary>
        /// Generate uuid for Study StudyDesigns
        /// </summary>
        /// <param name="studyDesigns"></param>
        /// <returns></returns>
        List<StudyDesignEntity> GenerateIdForStudyDesign(List<StudyDesignEntity> studyDesigns);
        /// <summary>
        /// Generate uuid for Study Estimands
        /// </summary>
        /// <param name="estimands"></param>
        /// <returns></returns>
        List<EstimandEntity> GenerateIdForStudyEstimand(List<EstimandEntity> estimands);
        /// <summary>
        /// Generate uuid for Study Identifiers
        /// </summary>
        /// <param name="studyIdentifiers"></param>
        /// <returns></returns>
        List<StudyIdentifierEntity> GenerateIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers);
        /// <summary>
        /// Generate uuid for Study Indications
        /// </summary>
        /// <param name="indications"></param>
        /// <returns></returns>
        List<IndicationEntity> GenerateIdForStudyIndications(List<IndicationEntity> indications);
        /// <summary>
        /// Generate uuid for Study Objectives
        /// </summary>
        /// <param name="objectives"></param>
        /// <returns></returns>
        List<ObjectiveEntity> GenerateIdForStudyObjectives(List<ObjectiveEntity> objectives);
        /// <summary>
        /// Generate uuid for Study Protocol Versions
        /// </summary>
        /// <param name="studyProtocolVersions"></param>
        /// <returns></returns>
        List<StudyProtocolVersionEntity> GenerateIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions);
        /// <summary>
        /// Generate uuid for Study Workflows
        /// </summary>
        /// <param name="workflows"></param>
        /// <returns></returns>
        List<WorkflowEntity> GenerateIdForStudyWorkflow(List<WorkflowEntity> workflows);
        #endregion
        /// <summary>
        /// Get Audit Trail fields for the POST Api
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        AuditTrailEntity GetAuditTrail(string user);

        #region Check whole study
        /// <summary>
        /// Compare Full Study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool IsSameStudy(StudyDefinitionsEntity incoming, StudyDefinitionsEntity existing);
        /// <summary>
        /// Deep compare of existing and incoming study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool JsonObjectCheck(object incoming, object existing);
        #endregion
        #region Remove Id for Each section
        /// <summary>
        /// Remode uuid for Study
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        StudyDefinitionsEntity RemovedSectionId(StudyDefinitionsEntity study);
        /// <summary>
        /// Remove uuid for Study Investigational Interventions
        /// </summary>
        /// <param name="investigationalInterventions"></param>
        /// <returns></returns>
        List<InvestigationalInterventionEntity> RemoveIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions);
        /// <summary>
        /// Remove uuid for Study Cells
        /// </summary>
        /// <param name="studyCells"></param>
        /// <returns></returns>
        List<StudyCellEntity> RemoveIdForStudyCells(List<StudyCellEntity> studyCells);
        /// <summary>
        /// Remove uuid for Study Designs
        /// </summary>
        /// <param name="studyDesigns"></param>
        /// <returns></returns>
        List<StudyDesignEntity> RemoveIdForStudyDesign(List<StudyDesignEntity> studyDesigns);
        /// <summary>
        /// Remove uuid for Study Estimands
        /// </summary>
        /// <param name="estimands"></param>
        /// <returns></returns>
        List<EstimandEntity> RemoveIdForStudyEstimand(List<EstimandEntity> estimands);
        /// <summary>
        /// Remove uuid for Study Identifier
        /// </summary>
        /// <param name="studyIdentifiers"></param>
        /// <returns></returns>
        List<StudyIdentifierEntity> RemoveIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers);
        /// <summary>
        /// Remove uuid for Study Indications
        /// </summary>
        /// <param name="indications"></param>
        /// <returns></returns>
        List<IndicationEntity> RemoveIdForStudyIndications(List<IndicationEntity> indications);
        /// <summary>
        /// Remove uuid for Study Objectives
        /// </summary>
        /// <param name="objectives"></param>
        /// <returns></returns>
        List<ObjectiveEntity> RemoveIdForStudyObjectives(List<ObjectiveEntity> objectives);
        /// <summary>
        /// Remove uuid for Study Protocol Versions
        /// </summary>
        /// <param name="studyProtocolVersions"></param>
        /// <returns></returns>
        List<StudyProtocolVersionEntity> RemoveIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions);
        /// <summary>
        /// Remove uuid for Study Workflows
        /// </summary>
        /// <param name="workflows"></param>
        /// <returns></returns>
        List<WorkflowEntity> RemoveIdForStudyWorkflow(List<WorkflowEntity> workflows);
        #endregion

    }
}