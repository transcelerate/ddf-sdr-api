namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EndpointDto : SyntaxTemplateDto
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.ENDPOINT);
        public string Purpose { get; set; }
        public CodeDto Level { get; set; }
    }
}
