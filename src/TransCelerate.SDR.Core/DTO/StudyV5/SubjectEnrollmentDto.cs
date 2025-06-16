namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class SubjectEnrollmentDto : GeographicScopeDto
    {
        public QuantityDto Quantity { get; set; }
        public StudySiteDto AppliesTo { get; set; } 
    }
}
