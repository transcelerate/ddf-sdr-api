namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class CodeDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.CodeId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string CodeSystem { get; set; }
        public string CodeSystemVersion { get; set; }
        public string Decode { get; set; }
    }
}
