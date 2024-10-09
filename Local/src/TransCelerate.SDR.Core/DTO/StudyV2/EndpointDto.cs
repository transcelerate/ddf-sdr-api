namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class EndpointDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.EndpointId)]
        public string Id { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public CodeDto EndpointLevel { get; set; }
    }
}
