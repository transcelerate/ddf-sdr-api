using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IClinicalStudyService
    {
        #region ActionMethods

        #region GET Methods

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetAllElements(string studyId,int version, string tag);

        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="sections">Study sections which have to be fetched</param>
        /// <returns>
        /// A <see cref="object"/> of study sections with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetSections(string studyId, int version, string tag, string[] sections);

        /// <summary>
        /// GET For a StudyDesign sections for a study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="tag">Tag of a study</param>
        /// <param name="studyDesignId">Study Design Id</param>
        /// <param name="sections">Study Design sections which have to be fetched</param>   
        /// <returns>
        /// A <see cref="object"/> of studyDesign sections with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetStudyDesignSections(string studyId,string studyDesignId, int version, string tag, string[] sections);

        /// <summary>
        /// GET Audit Trial
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetAuditTrail(DateTime fromDate, DateTime toDate, string studyId);

        /// <summary>
        /// Get AllStudy Id's
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
        /// <returns>
        /// A <see cref="GetStudyHistoryResponseDTO"/> which has list of study ID's <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<GetStudyHistoryResponseDTO> GetAllStudyId(DateTime fromDate, DateTime toDate,string studyTitle);
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>
        /// <param name="entrySystem">System which made the request</param>        
        /// <returns>
        /// A <see cref="PostStudyDTO"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        Task<object> PostAllElements(PostStudyDTO studyDTO, string entrySystem);

        /// <summary>
        /// Search Study Elements with search criteria
        /// </summary>
        /// <param name="searchParametersDTO">Parameters to search in database</param>
        /// <returns>
        /// A <see cref="List{GetStudyDTO}"/> which matches serach criteria <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        Task<List<GetStudyDTO>> SearchStudy(SearchParametersDTO searchParametersDTO);
        #endregion
        #endregion

        #region User Group Mapping
        /// <summary>
        /// Check access for the study
        /// </summary>
        /// <param name="study">Study for which user access have to be checked</param>   
        /// <returns>
        /// A <see cref="StudyEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        Task<StudyEntity> CheckAccessForAStudy(StudyEntity study);

        /// <summary>
        /// Check READ_WRITE Permission for a user
        /// </summary>        
        /// <returns>
        /// <see langword="true"/> If the user have READ_WRITE access in any of the groups <br></br> <br></br>
        /// <see langword="false"/> If the user does not have READ_WRITE access in any of the groups
        /// </returns>
        Task<bool> CheckPermissionForAUser();
        #endregion
    }
}
