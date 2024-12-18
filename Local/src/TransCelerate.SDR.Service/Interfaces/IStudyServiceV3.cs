using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV3;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IStudyServiceV3
    {
        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetStudy(string studyId, int sdruploadversion, LoggedInUser user);

        /// <summary>
        /// GET Partial Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of elements with comma separated values</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetPartialStudyElements(string studyId, int sdruploadversion, LoggedInUser user, string[] listofelements);

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of study design elements</param>
        /// <param name="studyDesignId">study design Id</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetStudyDesigns(string studyId, string studyDesignId, int sdruploadversion, LoggedInUser user, string[] listofelements);

        /// <summary>
        /// GET SoA
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="scheduleTimelineId">workdflowId</param>
        /// <param name="studyDesignId">study design Id</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetSOAV3(string studyId, string studyDesignId, string scheduleTimelineId, int sdruploadversion, LoggedInUser user);
        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of study design elements</param>
        /// <param name="studyDesignId">study design Id</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetPartialStudyDesigns(string studyId, string studyDesignId, int sdruploadversion, LoggedInUser user, string[] listofelements);

        /// <summary>
        /// GET eCPT Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="studyDesignId">studyDesignId</param>
        /// <param name="user">Logged in user</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GeteCPTV3(string studyId, int sdruploadversion, string studyDesignId, LoggedInUser user);

        /// <summary>
        /// GET Differences between two versions of a study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdrUploadVersionOne">First Version of study</param> 
        /// <param name="sdrUploadVersionTwo">Second Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<object> GetDifferences(string studyId, int sdrUploadVersionOne, int sdrUploadVersionTwo, LoggedInUser user);
        #endregion


        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="user">Logged In User</param>
        /// <param name="method">POST/PUT</param>
        /// <returns>
        /// A <see cref="object"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        Task<object> PostAllElements(StudyDefinitionsDto studyDTO, LoggedInUser user, string method);
        #endregion

        #region Delete Study
        /// <summary>
        /// Delete all versions of Study
        /// </summary>
        /// <param name="studyId">Study Id</param>
        /// <param name="user">LoggedIn User</param>
        /// <returns></returns>
        Task<object> DeleteStudy(string studyId, LoggedInUser user);
        #endregion

        #region Check Access For A study
        /// <summary>
        /// Check access for the study
        /// </summary>
        /// <param name="study">Study for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        Task<StudyDefinitionsEntity> CheckAccessForAStudy(StudyDefinitionsEntity study, LoggedInUser user);
        /// <summary>
        /// Check Access for a study
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> GetAccessForAStudy(string studyId, int sdruploadversion, LoggedInUser user);
        #endregion
    }
}
