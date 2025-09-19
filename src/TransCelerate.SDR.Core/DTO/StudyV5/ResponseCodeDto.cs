namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ResponseCodeDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public object IsEnabled { get; set; }
        public CodeDto Code { get; set; }
        public string InstanceType { get; set; }
    }
}
