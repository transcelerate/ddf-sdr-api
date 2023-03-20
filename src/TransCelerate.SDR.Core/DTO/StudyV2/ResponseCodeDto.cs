namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ResponseCodeDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ResponseCodeId)]
        public string Id { get; set; }
        public object ResponseCodeEnabled { get; set; }
        public CodeDto Code { get; set; }
    }
}
