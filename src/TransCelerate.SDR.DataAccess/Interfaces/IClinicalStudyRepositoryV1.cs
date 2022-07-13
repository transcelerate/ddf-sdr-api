using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Entities.UserGroups;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IClinicalStudyRepositoryV1
    {       

        /// <summary>
        /// GET a Study for a study ID with version filter
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        Task<StudyEntity> GetStudyItemsAsync(string studyId, int version);


        /// <summary>
        /// POST a Study
        /// </summary>
        /// <param name="study">Study for Inserting into Database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> PostStudyItemsAsync(StudyEntity study);

        /// <summary>
        /// Updates a Study
        /// </summary>
        /// <param name="study">Update study in database</param>
        /// <returns>
        /// A studyId which was inserted <br></br> <br></br>        
        /// </returns>
        Task<string> UpdateStudyItemsAsync(StudyEntity study);

        Task<List<SDRGroupsEntity>> GetGroupsOfUser(LoggedInUser user);
    }
}
