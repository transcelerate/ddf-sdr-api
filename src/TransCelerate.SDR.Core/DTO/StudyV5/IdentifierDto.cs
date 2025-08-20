namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class IdentifierDto : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public OrganizationDto Scope { get; set; }
        public string InstanceType { get; set; }
    }
}
