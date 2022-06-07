using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IClinicalStudyRepository
    {
        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<StudyEntity> GetStudyItemsAsync(string studyId,int version);

        /// <summary>
        /// GET a Study for a study ID with version and tag filter
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="version"></param>
        /// <param name="tag"></param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<StudyEntity> GetStudyItemsAsync(string studyId, int version, string tag);

        /// <summary>
        /// GET List of study for a study ID
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="study"></param>
        /// <returns>
        /// A <see cref="List{StudyEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<StudyEntity>> GetAuditTrail(DateTime fromDate, DateTime toDate, string study);

        /// <summary>
        /// Get List of all studyId 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyTitle"></param>
        /// <returns>
        /// A <see cref="List{StudyHistoryEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<StudyHistoryEntity>> GetAllStudyId(DateTime fromDate, DateTime toDate, string studyTitle);

        /// <summary>
        /// POST a Study
        /// </summary>
        /// <param name="study"></param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> PostStudyItemsAsync(StudyEntity study);

        /// <summary>
        /// Search the collection based on search criteria
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns>
        /// A <see cref="List{SearchResponseEntity}"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<List<SearchResponse>> SearchStudy(SearchParameters searchParameters);

        /// <summary>
        /// Updates a Study
        /// </summary>
        /// <param name="study"></param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> UpdateStudyItemsAsync(StudyEntity study);
    }   
}
