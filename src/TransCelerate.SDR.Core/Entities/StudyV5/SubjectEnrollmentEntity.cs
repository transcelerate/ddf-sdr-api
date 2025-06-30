namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class SubjectEnrollmentEntity : GeographicScopeEntity
    {
        public QuantityEntity Quantity { get; set; }
        public GeographicScopeEntity ForGeographicScope { get; set; }
        public StudyCohortEntity ForStudyCohort { get; set; } 
        public StudySiteEntity ForStudySite { get; set; }   
    }
}
