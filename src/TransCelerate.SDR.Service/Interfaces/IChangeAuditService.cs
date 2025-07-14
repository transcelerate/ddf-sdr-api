using System.Threading.Tasks;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IChangeAuditService
    {
        /// <summary>
        /// Get Change Audit for a StudyId
        /// </summary>
        /// <param name="studyId"></param>
        /// <returns></returns>
        Task<object> GetChangeAudit(string studyId);
    }
}