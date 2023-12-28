namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class GeographicScopeDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Type { get; set; }
        public CodeDto Code { get; set; }
    }
}
