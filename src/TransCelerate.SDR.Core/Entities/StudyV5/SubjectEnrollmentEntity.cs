namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class SubjectEnrollmentEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public QuantityEntity Quantity { get; set; }
        public GeographicScopeEntity ForGeographicScope { get; set; }
        public StudyCohortEntity ForStudyCohort { get; set; } 
        public StudySiteEntity ForStudySite { get; set; }   
    }
}
