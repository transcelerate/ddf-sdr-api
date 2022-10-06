using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.AzureFunctions.DataAccess
{
    public interface IChangeAuditReposotory
    {
        ChangeAuditEntity GetChangeAuditAsync(string studyId);
        List<StudyEntity> GetStudyItemsAsync(string studyId, int sdruploadversion);
        void InsertChangeAudit(ChangeAuditEntity changeAudit);
        void UpdateChangeAudit(ChangeAuditEntity changeAudit);
    }
}