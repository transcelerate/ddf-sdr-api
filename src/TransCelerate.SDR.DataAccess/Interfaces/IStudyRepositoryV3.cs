using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV3;
using TransCelerate.SDR.Core.Entities.UserGroups;


namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IStudyRepositoryV3
    {

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<StudyDefinitionsEntity> GetStudyItemsAsync(string studyId, int sdruploadversion);

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelementsArray">Array of study elements</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<StudyDefinitionsEntity> GetPartialStudyItemsAsync(string studyId, int sdruploadversion, string[] listofelementsArray);

        /// <summary>
        /// GET Study Designs for a Study Id
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns></returns>
        Task<StudyDefinitionsEntity> GetPartialStudyDesignItemsAsync(string studyId, int sdruploadversion);
        
        /// <summary>
        /// POST a Study
        /// </summary>
        /// <param name="study">Study for Inserting into Database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> PostStudyItemsAsync(StudyDefinitionsEntity study);

        /// <summary>
        /// Updates a Study
        /// </summary>
        /// <param name="study">Update study in database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> UpdateStudyItemsAsync(StudyDefinitionsEntity study);

        Task<List<SDRGroupsEntity>> GetGroupsOfUser(LoggedInUser user);
        /// <summary>
        /// Delete all version of a study
        /// </summary>
        /// <param name="study_uuid">Study Id</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteStudyAsync(string study_uuid);

        /// <summary>
        /// Count Documents
        /// </summary>
        /// <param name="study_uuid"> Study Id</param>
        /// <returns></returns>
        Task<long> CountAsync(string study_uuid);

        Task<StudyDefinitionsEntity> GetStudyItemsForCheckingAccessAsync(string studyId, int sdruploadversion);

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <returns>
        /// A <see cref="AuditTrailEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<AuditTrailEntity> GetUsdmVersionAsync(string studyId, int sdruploadversion);
    }
}
