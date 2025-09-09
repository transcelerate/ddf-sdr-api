namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class IdentifierDto : IId
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ScopeId { get; set; }
        public string InstanceType { get; set; }
    }
}
