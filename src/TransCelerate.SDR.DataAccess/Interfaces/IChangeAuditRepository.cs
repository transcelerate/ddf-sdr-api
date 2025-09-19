using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Common;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IChangeAuditRepository
    {
        Task<ChangeAuditStudyEntity> GetChangeAuditAsync(string studyId);

        Task<string> InsertChangeAudit(string study_uuid, int sdruploadversion, int sdruploadflag, DateTime entrydatetime);//****** Added By basha

        Task<string> AddOrUpdateChangeAuditAsync(string studyId, List<string> changedValues, AuditTrailEntity currentVersionAuditTrail);

        Task<List<Core.Entities.StudyV3.StudyDefinitionsEntity>> GetStudyItemsAsyncV3(string studyId, int sdruploadversion);

        Task<List<Core.Entities.StudyV4.StudyDefinitionsEntity>> GetStudyItemsAsyncV4(string studyId, int sdruploadversion);

        Task<List<Core.Entities.StudyV5.StudyDefinitionsEntity>> GetStudyItemsAsyncV5(string studyId, int sdruploadversion);
    }
}