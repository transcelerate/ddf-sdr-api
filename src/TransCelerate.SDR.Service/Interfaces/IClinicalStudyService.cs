using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.Services.Interfaces
{
    public interface IClinicalStudyService
    {
        #region ActionMethods

        #region GET Methods

        //GET All Elements For a Study
        Task<GetStudyDTO> GetAllElements(string studyId,int version, string tag);

        //GET All Elements For a Study
        Task<GetStudySectionsDTO> GetSections(string studyId, int version, string tag, string[] sections);

        //GET For a StudyDesign sections for a study
        Task<GetStudySectionsDTO> GetStudyDesignSections(string studyId,string studyDesignId, int version, string tag, string[] sections);

        //GET AuditTrail For a Study
        Task<GetStudyAuditDTO> GetAuditTrail(DateTime fromDate, DateTime toDate, string study);

        //GET All studyId's
        Task<GetStudyHistoryResponseDTO> GetAllStudyId(DateTime fromDate, DateTime toDate);
        #endregion

        #region POST Methods
        //POST All Elements For a Study
        Task<object> PostAllElements(PostStudyDTO study,string entrySystem);

        //Search For a Study
        Task<List<GetStudyDTO>> SearchStudy(SearchParametersDTO searchParameters);
        #endregion
        #endregion        

    }
}
