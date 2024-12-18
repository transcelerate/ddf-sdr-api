namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ResponseCodeDto : IId
    {        
        public string Id { get; set; }
        public object IsEnabled { get; set; }
        public CodeDto Code { get; set; }
        public string InstanceType { get; set; }
    }
}
