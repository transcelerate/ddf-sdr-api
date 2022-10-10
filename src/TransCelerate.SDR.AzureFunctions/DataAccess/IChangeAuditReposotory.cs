using System.Collections.Generic;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.AzureFunctions.DataAccess
{
    public interface IChangeAuditReposotory
    {
        /// <summary>
        /// Get Audit Details for a Study Id from Change Audit Collections
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <returns> A <see cref="ChangeAuditEntity"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        ChangeAuditStudyEntity GetChangeAuditAsync(string studyId);
        /// <summary>
        /// Get Current and previous version of study for study Id
        /// </summary>
        /// <param name="studyId">Study UUID</param>
        /// <param name="sdruploadversion">current version</param>
        /// <returns> A <see cref="List{StudyEntity}"/> with matching studyId
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        List<StudyEntity> GetStudyItemsAsync(string studyId, int sdruploadversion);
        /// <summary>
        /// Insert a Change Audit for a study
        /// </summary>
        /// <param name="changeAudit"></param>
        void InsertChangeAudit(ChangeAuditStudyEntity changeAudit);
        /// <summary>
        /// Update existing change audit
        /// </summary>
        /// <param name="changeAudit"></param>
        void UpdateChangeAudit(ChangeAuditStudyEntity changeAudit);
    }
}