namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class CodeDto : IUuid
    {
        public string Uuid { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
}
