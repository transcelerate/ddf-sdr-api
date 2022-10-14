using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IChangeAuditRepository
    {
        Task<ChangeAuditStudyEntity> GetChangeAuditAsync(string studyId);
    }
}