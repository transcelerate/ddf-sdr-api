namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class TimingDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.TimingId)]
        public string Id { get; set; }
        public CodeDto TimingType { get; set; }
        public string TimingValue { get; set; }
        public string TimingWindow { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public CodeDto TimingRelativeToFrom { get; set; }
    }
}
