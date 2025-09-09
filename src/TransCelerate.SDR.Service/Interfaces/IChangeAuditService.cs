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

        /// <summary>
        /// Process Change Audit for a study given study ID
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="currentVersion">Version of Study</param>
        /// <returns></returns>
        Task<string> ProcessChangeAudit(string studyId, int currentVersion);
    }
}