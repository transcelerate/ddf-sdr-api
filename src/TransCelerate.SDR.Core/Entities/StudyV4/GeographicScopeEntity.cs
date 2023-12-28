namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class GeographicScopeEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Type { get; set; }
        public CodeEntity Code { get; set; }
    }
}
