namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class CodeEntity : IUuid
    {
        public string Uuid { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }        
    }
}
