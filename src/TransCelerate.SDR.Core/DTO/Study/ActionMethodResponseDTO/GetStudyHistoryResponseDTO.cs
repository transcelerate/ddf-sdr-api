using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for all study Id
    /// </summary>
    public class GetStudyHistoryResponseDTO
    {
        /// <summary>
        /// This property holds the Study History details
        /// </summary>
        public List<StudyHistoryDTO> study { get; set; }
    }  
    
    public class StudyHistoryDTO
    {
        public string studyTitle { get; set; }
        public string studyId { get; set; }
        public int[] studyVersion { get; set; }      
    }
}
