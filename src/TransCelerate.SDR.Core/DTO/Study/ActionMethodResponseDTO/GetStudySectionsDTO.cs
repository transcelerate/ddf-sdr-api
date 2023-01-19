using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for Study Level sections
    /// </summary>
    public class GetStudySectionsDTO
    {
        /// <summary>
        /// This property holds the value of Study ID
        /// </summary>
        public string studyId { get; set; }
        /// <summary>
        /// This property holds the value of version of Study
        /// </summary>        
        [JsonProperty("SDRUploadVersion")]
        public int studyVersion { get; set; }

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
        public TransCelerate.SDR.Core.DTO.Common.LinksDto Links { get; set; }

    }
}
