namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class SubjectEnrollmentDto : GeographicScopeDto
    {
        public QuantityDto Quantity { get; set; }
        public GeographicScopeDto ForGeographicScope { get; set; } 
        public StudyCohortDto ForStudyCohort { get; set; } 
        public StudySiteDto ForStudySite { get; set; } 
    }
}
