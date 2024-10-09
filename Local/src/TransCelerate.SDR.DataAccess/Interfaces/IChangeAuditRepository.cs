using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Common;  

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IChangeAuditRepository
    {
        Task<ChangeAuditStudyEntity> GetChangeAuditAsync(string studyId);
        Task<string> InsertChangeAudit(string study_uuid, int sdruploadversion, DateTime entrydatetime);
    }
}