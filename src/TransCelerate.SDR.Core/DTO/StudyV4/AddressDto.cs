namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class AddressDto : IId
    {        
        public string Id { get; set; }
        public string Text { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public CodeDto Country { get; set; }
        public string InstanceType { get; set; }

    }
}
