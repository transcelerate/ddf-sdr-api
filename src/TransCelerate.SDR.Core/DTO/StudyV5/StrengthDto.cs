namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StrengthDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public QuantityRangeDto Numerator { get; set; }
        public QuantityDto Denominator { get; set; }
    }
}
