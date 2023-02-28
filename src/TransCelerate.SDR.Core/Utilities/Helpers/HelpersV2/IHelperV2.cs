﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2
{
    public interface IHelperV2
    {
        /// <summary>
        /// Get Audit Trail fields for the POST Api
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        AuditTrailEntity GetAuditTrail(string user);

        #region Partial Study Elements
        /// <summary>
        /// Check whether the the input list of elements are valid or not
        /// </summary>
        /// <param name="listofelements"></param>
        /// <param name="listofelementsArray"></param>
        /// <returns></returns>
        bool AreValidStudyElements(string listofelements, out string[] listofelementsArray);

        /// <summary>
        /// Check whether the the input list of study design elements are valid or not
        /// </summary>
        /// <param name="listofelements"></param>
        /// <param name="listofelementsArray"></param>
        /// <returns></returns>
        bool AreValidStudyDesignElements(string listofelements, out string[] listofelementsArray);
        /// <summary>
        /// Remove the study elemets which are not requested
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="studyDTO"></param>
        /// <returns></returns>
        object RemoveStudyElements(string[] sections, StudyDto studyDTO);
        /// <summary>
        /// Remove studyDesign elements which are not requested
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="studyDTO"></param>
        /// <param name="study_uuid"></param>
        /// <returns></returns>
        object RemoveStudyDesignElements(string[] sections, List<StudyDesignDto> studyDTO, string study_uuid);
        #endregion
        #region Check whole study
        /// <summary>
        /// Compare Full Study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool IsSameStudy(StudyEntity incoming, StudyEntity existing);
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
        StudyEntity RemovedSectionId(StudyEntity study);
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
        #endregion

        #region Get Difference
        List<string> GetChangedValues(StudyEntity currentStudyVersion, StudyEntity previousStudyVersion);
        #endregion

        #region RefernceIntegrityCheck
        bool ReferenceIntegrityValidation(StudyDto study, out object referenceErrors);

        #endregion

    }
}