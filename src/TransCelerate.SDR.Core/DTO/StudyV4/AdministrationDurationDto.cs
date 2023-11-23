namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class AdministrationDurationDto : IId
    {
        public string Id { get; set; }
        public QuantityDto Quantity { get; set; }
        public string Description { get; set; }
        public bool DurationWillVary { get; set; }
        public string ReasonDurationWillVary { get; set; }
    }
}
