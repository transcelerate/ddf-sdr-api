namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class SubjectEnrollmentDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public QuantityDto Quantity { get; set; }
        public GeographicScopeDto ForGeographicScope { get; set; } 
        public string ForStudyCohortId { get; set; } 
        public string ForStudySiteId { get; set; } 
    }
}
