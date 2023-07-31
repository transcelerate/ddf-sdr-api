namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ResponseCodeDto : IId
    {        
        public string Id { get; set; }
        public object ResponseCodeEnabled { get; set; }
        public CodeDto Code { get; set; }
    }
}
