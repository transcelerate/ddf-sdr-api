namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class GeographicScopeDto : IId
    {
        public string Id { get; set; }        
        public CodeDto Type { get; set; }
        public AliasCodeDto Code { get; set; }
        public string InstanceType { get; set; }
    }
}
