using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Entities.StudyV5;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5
{
    public interface IHelperV5
    {
        /// <summary>
        /// Get Audit Trail fields for the POST Api
        /// </summary>
        /// <param name="user"></param>
        /// <param name="usdmVersion"></param>
        /// <returns></returns>
        AuditTrailEntity GetAuditTrail(string user, string usdmVersion);
        /// <summary>
        /// JSON Serializer for camel casing
        /// </summary>
        /// <returns></returns>
        JsonSerializerSettings GetSerializerSettingsForCamelCasing();

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
        object RemoveStudyElements(string[] sections, StudyDefinitionsDto studyDTO);
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
        bool IsSameStudy(StudyDefinitionsEntity incoming, StudyDefinitionsEntity existing);
        /// <summary>
        /// Deep compare of existing and incoming study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool JsonObjectCheck(object incoming, object existing);
        #endregion

        #region Get Difference
        List<string> GetChangedValues(StudyDefinitionsEntity currentStudyVersion, StudyDefinitionsEntity previousStudyVersion);
        #endregion

        #region RefernceIntegrityCheck
        bool ReferenceIntegrityValidation(StudyDefinitionsDto study, out object referenceErrors);

        #endregion

        #region Version Compare
        List<string> GetChangedValuesForStudyComparison(StudyDefinitionsEntity currentStudyVersion, StudyDefinitionsEntity previousStudyVersion);
        #endregion

    }
}