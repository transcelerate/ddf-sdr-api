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
        public List<StudyHistoryDTO> Study { get; set; }
    }

    public class StudyHistoryDTO
    {
        public string StudyTitle { get; set; }
        public string StudyId { get; set; }
        public int[] StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
