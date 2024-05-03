namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class TimingDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public CodeDto Type { get; set; }
        public string Value { get; set; }
        public string ValueLabel { get; set; }
        public string Description { get; set; }
        public string WindowLabel { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public string WindowLower { get; set; }
        public string WindowUpper { get; set; }
        public CodeDto RelativeToFrom { get; set; }
        public string InstanceType { get; set; }
    }
}
