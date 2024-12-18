namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class AdministrationDurationEntity : IId
    {
        public string Id { get; set; }
        public QuantityEntity Quantity { get; set; }
        public string Description { get; set; }
        public bool DurationWillVary { get; set; }
        public string ReasonDurationWillVary { get; set; }
        public string InstanceType { get; set; }
    }
}
