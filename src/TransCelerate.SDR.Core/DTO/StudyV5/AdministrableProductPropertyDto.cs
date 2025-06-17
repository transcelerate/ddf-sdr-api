namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AdministrableProductPropertyDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public QuantityDto Quantity { get; set; }
        public CodeDto Type { get; set; }
    }
}
