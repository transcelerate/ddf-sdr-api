namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class CodeDto : IId
    {        
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
        public string InstanceType { get; set; }
    }
}
