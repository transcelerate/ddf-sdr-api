namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for GET Method for all elements of a study
    /// </summary>
    public class GetStudyDTO
    {
        //public string _id { get; set; }
        public GetClinicalStudyDTO clinicalStudy { get; set; }
        public AuditTrailDTO auditTrail { get; set; }
    }
}
