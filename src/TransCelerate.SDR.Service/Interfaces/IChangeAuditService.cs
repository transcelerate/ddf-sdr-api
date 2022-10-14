using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IChangeAuditService
    {
        /// <summary>
        /// Get Change Audit for a StudyId
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<object> GetChangeAudit(string studyId, LoggedInUser user);
    }
}