namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    public class StudyEntity
    {
        public object Id { get; set; }
        public ClinicalStudyEntity ClinicalStudy { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }
}
