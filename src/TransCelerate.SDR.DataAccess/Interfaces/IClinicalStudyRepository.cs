using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.DataAccess.Interfaces
{
    public interface IClinicalStudyRepository
    {
        Task<StudyEntity> GetStudyItemsAsync(string studyId,int version); 
        Task<StudyEntity> GetStudyItemsAsync(string studyId, int version, string tag);
        Task<List<StudyEntity>> GetAuditTrail(DateTime fromDate, DateTime toDate, string study);
        Task<object> GetAllStudyId(DateTime fromDate, DateTime toDate);
        Task<string> PostStudyItemsAsync(StudyEntity study); 
        Task<List<StudyEntity>> SearchStudy(SearchParameters searchParameters);
        Task<string> UpdateStudyItemsAsync(StudyEntity study);
    }   
}
