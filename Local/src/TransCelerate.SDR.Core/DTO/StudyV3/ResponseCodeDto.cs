namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ResponseCodeDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.ResponseCodeId)]
        public string Id { get; set; }
        public object ResponseCodeEnabled { get; set; }
        public CodeDto Code { get; set; }
    }
}
