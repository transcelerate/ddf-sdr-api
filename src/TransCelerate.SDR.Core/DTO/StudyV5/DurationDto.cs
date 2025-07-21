namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class DurationDto : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public QuantityRangeDto Quantity { get; set; }
        public bool DurationWillVary { get; set; }
        public string ReasonDurationWillVary { get; set; }
    }
}
