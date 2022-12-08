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
    }
}
