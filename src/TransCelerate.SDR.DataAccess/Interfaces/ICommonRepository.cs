using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface ICommonRepository
    {
        Task<GetRawJsonEntity> GetStudyItemsAsync(string studyId, int sdruploadversion);

        Task<List<SDRGroupsEntity>> GetGroupsOfUser(LoggedInUser user);

        /// <summary>
        /// GET UsdmVersion
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<string> GetUsdmVersion(string studyId, int sdruploadversion);

        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<AuditTrailResponseEntity>> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">LoggedIn User</param>        
        /// <returns>
        /// A <see cref="List{SearchTitleResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<SearchTitleResponseEntity>> SearchTitle(SearchTitleParametersEntity searchParameters, LoggedInUser user);

        /// <summary>
        /// Get List of all studyId 
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>        
        /// <returns>
        /// A <see cref="List{StudyHistoryEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<StudyHistoryResponseEntity>> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle);

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters">Parameters to search in database</param>        
        /// <param name="user">Loggedin User</param>        
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<SearchResponseEntity>> SearchStudy(SearchParametersEntity searchParameters, LoggedInUser user);
    }
}
