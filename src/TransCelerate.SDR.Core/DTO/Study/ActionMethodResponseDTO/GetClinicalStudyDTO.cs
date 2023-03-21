using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for all elements of clinicalStudy
    /// </summary>
    public class GetClinicalStudyDTO
    {
        /// <summary>
        /// This property holds the value of Study ID
        /// </summary>
        public string StudyId { get; set; }
        /// <summary>
        /// This property holds the value of Study Title for specific <see cref="StudyId"/>
        /// </summary>
        public string StudyTitle { get; set; }
        /// <summary>
        /// This property holds the value of Study Type for specific <see cref="StudyId"/>
        /// </summary>
        public string StudyType { get; set; }

        /// <summary>
        /// This property holds the value of Study Phase for specific <see cref="StudyId"/>
        /// </summary>
        public string StudyPhase { get; set; }
        /// <summary>
        /// This property holds the value of Study Status for specific <see cref="StudyId"/>
        /// </summary>
        public string StudyStatus { get; set; }
        /// <summary>
        /// This property holds the value of Study Tag for specific <see cref="StudyId"/>
        /// </summary>
        public string StudyTag { get; set; }
        /// <summary>
        /// This property holds the List of Study Identifiers for specific <see cref="StudyId"/>
        /// </summary>
        public List<StudyIdentifierDTO> StudyIdentifiers { get; set; }
        /// <summary>
        /// This property holds the List of Study Protocol References for specific <see cref="StudyId"/>
        /// </summary>
        public List<StudyProtocolDTO> StudyProtocolReferences { get; set; }
        /// <summary>
        /// This property holds the List of Study Designs for specific <see cref="StudyId"/>
        /// </summary>
        public List<GetStudyDesignsDTO> StudyDesigns { get; set; }
        /// <summary>
        /// This property holds the List of Study Objectives for specific <see cref="StudyId"/>
        /// </summary>
        public List<StudyObjectiveDTO> Objectives { get; set; }
        /// <summary>
        /// This property holds the List of Study Indications for specific <see cref="StudyId"/>
        /// </summary>
        public List<StudyIndicationDTO> StudyIndications { get; set; }
    }
}
