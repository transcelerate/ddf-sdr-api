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
        #region Depricated Methods
        ////GET InterventionModel For a Study
        //Task<InterventionModelResponse> InterventionModel(string study, string version, string status);

        ////GET Investigationalinterventions For a Study

        //Task<object> Investigationalinterventions(string study, string version, string status);

        ////GET StudyIdentifiers For a Study

        //Task<object> StudyIdentifiers(string study, string version, string status);
        ////GET StudyPhase For a Study

        //Task<StudyPhaseResponse> StudyPhase(string study, string version, string status);

        ////GET StudyProtocol For a Study

        //Task<object> StudyProtocol(string study, string version, string status);

        ////GET StudyObjectives For a Study

        //Task<object> StudyObjectives(string study, string version, string status);

        ////GET StudyTargetPopulation For a Study

        //Task<object> StudyTargetPopulation(string study, string version, string status);

        ////GET StudyTitle For a Study

        //Task<StudyTitleResponse> StudyTitle(string study, string version, string status);

        ////GET StudyIndication For a Study

        //Task<object> StudyIndication(string study, string version, string status);

        ////GET StudyType For a Study       
        //Task<StudyTypeResponse> StudyType(string study, string version, string status); 
        #endregion

        //GET All Elements For a Study
        Task<object> GetAllElements(string studyId,int version, string tag);

        //GET All Elements For a Study
        Task<object> GetSections(string studyId, int version, string tag, string[] sections);

        //GET For a StudyDesign sections for a study
        Task<object> GetStudyDesignSections(string studyId,string studyDesignId, int version, string tag, string[] sections);

        //GET AuditTrail For a Study
        Task<object> GetAuditTrail(DateTime fromDate, DateTime toDate, string study);
        #endregion

        #region POST Methods
        //POST All Elements For a Study
        Task<object> PostAllElements(PostStudyDTO study,string entrySystem,string entrySystemId);

        //Search For a Study
        Task<object> SearchStudy(SearchParametersDTO searchParameters);
        #endregion
        #endregion
    }
}
