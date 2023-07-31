namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EndpointDto : IId
    {        
        public string Id { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public CodeDto EndpointLevel { get; set; }
    }
}
