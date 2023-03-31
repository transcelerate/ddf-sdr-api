namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class AddressDto
    {
        public string Text { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public CodeDto Country { get; set; }

    }
}
