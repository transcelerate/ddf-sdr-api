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
        public string StudyId { get; set; }
        /// <summary>
        /// This property holds the value of version of Study
        /// </summary>        
        [JsonProperty(nameof(DTO.Common.AuditTrailDto.SDRUploadVersion))]
        public int StudyVersion { get; set; }

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
        public TransCelerate.SDR.Core.DTO.Common.LinksDto Links { get; set; }

    }
}
