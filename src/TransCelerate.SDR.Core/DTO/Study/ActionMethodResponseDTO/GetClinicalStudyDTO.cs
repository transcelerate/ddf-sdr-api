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
        public string studyId { get; set; }
        /// <summary>
        /// This property holds the value of Study Title for specific <see cref="studyId"/>
        /// </summary>
        public string studyTitle { get; set; }
        /// <summary>
        /// This property holds the value of Study Type for specific <see cref="studyId"/>
        /// </summary>
        public string studyType { get; set; }

        /// <summary>
        /// This property holds the value of Study Phase for specific <see cref="studyId"/>
        /// </summary>
        public string studyPhase { get; set; }
        /// <summary>
        /// This property holds the value of Study Status for specific <see cref="studyId"/>
        /// </summary>
        public string studyStatus { get; set; }
        /// <summary>
        /// This property holds the value of Study Tag for specific <see cref="studyId"/>
        /// </summary>
        public string studyTag { get; set; }
        /// <summary>
        /// This property holds the List of Study Identifiers for specific <see cref="studyId"/>
        /// </summary>
        public List<StudyIdentifierDTO> studyIdentifiers { get; set; }
        /// <summary>
        /// This property holds the List of Study Protocol References for specific <see cref="studyId"/>
        /// </summary>
        public List<StudyProtocolDTO> studyProtocolReferences { get; set; }
        /// <summary>
        /// This property holds the List of Study Designs for specific <see cref="studyId"/>
        /// </summary>
        public List<GetStudyDesignsDTO> studyDesigns { get; set; }
        /// <summary>
        /// This property holds the List of Study Objectives for specific <see cref="studyId"/>
        /// </summary>
        public List<StudyObjectiveDTO> objectives { get; set; }
        /// <summary>
        /// This property holds the List of Study Indications for specific <see cref="studyId"/>
        /// </summary>
        public List<StudyIndicationDTO> studyIndications { get; set; }           
    }
}
