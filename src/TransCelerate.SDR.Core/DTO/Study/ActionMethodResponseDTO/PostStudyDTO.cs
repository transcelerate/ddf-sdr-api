using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.Core.DTO
{
    /// <summary>
    /// This class is a DTO for POST Method for a study
    /// </summary>
    public class PostStudyDTO
    {
        /// <summary>
        /// This property holds the ClinicalStudy Component of the Study for POST Method
        /// </summary>
        public ClinicalStudyDTO ClinicalStudy { get; set; }
        /// <summary>
        /// This property holds the Audit Trail Component of the Study for POST Method
        /// </summary>
        public AuditTrailDTO AuditTrail { get; set; }

        public object Links { get; set; }
    }
}
