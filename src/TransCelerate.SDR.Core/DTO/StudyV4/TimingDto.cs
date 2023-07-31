namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class TimingDto : IId
    {        
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
