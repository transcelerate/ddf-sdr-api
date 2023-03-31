namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for all elements of a study
    /// </summary>
    public class GetStudyDTO
    {
        //public string _id { get; set; }
        /// <summary>
        /// This property holds the ClinicalStudy Component of the Study 
        /// </summary>
        public GetClinicalStudyDTO ClinicalStudy { get; set; }
        /// <summary>
        /// This property holds the Audit Trail Component of the Study 
        /// </summary>
        public AuditTrailDTO AuditTrail { get; set; }
        public TransCelerate.SDR.Core.DTO.Common.LinksForUIDto Links { get; set; }
    }
}
