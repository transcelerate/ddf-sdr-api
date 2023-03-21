namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchTitleDTO
    {
        public SearchTitleClinicalStudy ClinicalStudy { get; set; }
        public SearchTitleAuditTrail AuditTrail { get; set; }
    }

    public class SearchTitleClinicalStudy
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyTag { get; set; }
    }

    public class SearchTitleAuditTrail
    {
        public string EntryDateTime { get; set; }
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
