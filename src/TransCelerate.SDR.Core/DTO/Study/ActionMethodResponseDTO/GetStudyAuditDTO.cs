using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for AuditTrail of a study
    /// </summary>
    public class GetStudyAuditDTO
    {
        /// <summary>
        /// This property holds the value of Study ID
        /// </summary>
        public string StudyId { get; set; }
        /// <summary>
        /// This property holds the List of Audit Trail for specific <see cref="StudyId"/>
        /// </summary>
        public List<AuditTrailEndpointResponseDTO> AuditTrail { get; set; }
    }

    public class AuditTrailEndpointResponseDTO
    {
        public string StudyTag { get; set; }
        public string StudyStatus { get; set; }
        public string EntryDateTime { get; set; }
        public string EntrySystem { get; set; }
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
