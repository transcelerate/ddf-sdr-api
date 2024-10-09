namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class TimingDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.TimingId)]
        public string Id { get; set; }
        public CodeDto TimingType { get; set; }
        public string TimingValue { get; set; }
        public string TimingDescription { get; set; }
        public string TimingWindow { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public string TimingWindowLower { get; set; }
        public string TimingWindowUpper { get; set; }
        public CodeDto TimingRelativeToFrom { get; set; }
    }
}
