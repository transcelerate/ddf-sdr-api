using System.Collections.Generic;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1
{
    public interface IHelper
    {
        List<CodeEntity> CheckForCodeSection(List<CodeEntity> incomingCodes, List<CodeEntity> existingCodes);
        List<DefinedProcedureEntity> CheckForDefinedProceduresSection(List<DefinedProcedureEntity> incomingDefinedProcedures, List<DefinedProcedureEntity> exisitingDefinedProcedures);
        List<InterCurrentEventEntity> CheckForIntercurrentEventsSection(List<InterCurrentEventEntity> incomingInterCurrentEvents, List<InterCurrentEventEntity> exisitingInterCurrentEvents);
        List<InvestigationalInterventionEntity> CheckForInvestigationalInterventionsSection(List<InvestigationalInterventionEntity> incomingInvestigationalInterventions, List<InvestigationalInterventionEntity> existingInvestigationalInterventions);
        StudyEntity CheckForSections(StudyEntity incoming, StudyEntity existing);
        List<StudyCellEntity> CheckForStudyCellsSection(List<StudyCellEntity> incomingStudyCells, List<StudyCellEntity> existingStudyCells);
        List<StudyDataCollectionEntity> CheckForStudyDataCollectionSection(List<StudyDataCollectionEntity> incomingStudyDataCollections, List<StudyDataCollectionEntity> existingStudyDataCollections);
        List<StudyDesignPopulationEntity> CheckForStudyDesignPopulationsSection(List<StudyDesignPopulationEntity> incomingStudyDesignPopulations, List<StudyDesignPopulationEntity> existingStudyDesignPopulations);
        List<StudyDesignEntity> CheckForStudyDesignSection(List<StudyDesignEntity> incomingStudyDesigns, List<StudyDesignEntity> existingStudyDesigns);
        List<StudyElementEntity> CheckForStudyElementsSection(List<StudyElementEntity> incomingStudyElements, List<StudyElementEntity> existingStudyElements);
        List<EstimandEntity> CheckForStudyEstimandSection(List<EstimandEntity> incomingEstimands, List<EstimandEntity> existingEstimands);
        List<StudyIdentifierEntity> CheckForStudyIdentifierSection(List<StudyIdentifierEntity> incomingStudyIdentifiers, List<StudyIdentifierEntity> existingStudyIdentifiers);
        List<IndicationEntity> CheckForStudyIndicationsSection(List<IndicationEntity> incomingIndications, List<IndicationEntity> exisitingIndications);
        List<EndpointEntity> CheckForStudyObjectivesEndpointsSection(List<EndpointEntity> incomingEndpoints, List<EndpointEntity> existingEndpoints);
        List<ObjectiveEntity> CheckForStudyObjectivesSection(List<ObjectiveEntity> incomingObjectives, List<ObjectiveEntity> existingObjectives);
        List<StudyProtocolVersionEntity> CheckForStudyProtocolSection(List<StudyProtocolVersionEntity> incomingStudyProtocolVersions, List<StudyProtocolVersionEntity> existingStudyProtocolVersions);
        List<WorkFlowItemEntity> CheckForStudyWorkflowItemsSection(List<WorkFlowItemEntity> incomingWorkflowItems, List<WorkFlowItemEntity> existingWorkflowItems);
        List<WorkflowEntity> CheckForStudyWorkflowSection(List<WorkflowEntity> incomingWorkflows, List<WorkflowEntity> existingWorkflows);
        StudyEntity GeneratedSectionId(StudyEntity study);
        List<InvestigationalInterventionEntity> GenerateIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions);
        List<StudyCellEntity> GenerateIdForStudyCells(List<StudyCellEntity> studyCells);
        List<StudyDesignEntity> GenerateIdForStudyDesign(List<StudyDesignEntity> studyDesigns);
        List<EstimandEntity> GenerateIdForStudyEstimand(List<EstimandEntity> estimands);
        List<StudyIdentifierEntity> GenerateIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers);
        List<IndicationEntity> GenerateIdForStudyIndications(List<IndicationEntity> indications);
        List<ObjectiveEntity> GenerateIdForStudyObjectives(List<ObjectiveEntity> objectives);
        List<StudyProtocolVersionEntity> GenerateIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions);
        List<WorkflowEntity> GenerateIdForStudyWorkflow(List<WorkflowEntity> workflows);
        AuditTrailEntity GetAuditTrail(string user);
        bool IsSameStudy(StudyEntity incoming, StudyEntity existing);
        bool JsonObjectCheck(object incoming, object existing);
        StudyEntity RemovedSectionId(StudyEntity study);
        List<InvestigationalInterventionEntity> RemoveIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions);
        List<StudyCellEntity> RemoveIdForStudyCells(List<StudyCellEntity> studyCells);
        List<StudyDesignEntity> RemoveIdForStudyDesign(List<StudyDesignEntity> studyDesigns);
        List<EstimandEntity> RemoveIdForStudyEstimand(List<EstimandEntity> estimands);
        List<StudyIdentifierEntity> RemoveIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers);
        List<IndicationEntity> RemoveIdForStudyIndications(List<IndicationEntity> indications);
        List<ObjectiveEntity> RemoveIdForStudyObjectives(List<ObjectiveEntity> objectives);
        List<StudyProtocolVersionEntity> RemoveIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions);
        List<WorkflowEntity> RemoveIdForStudyWorkflow(List<WorkflowEntity> workflows);
    }
}