using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
