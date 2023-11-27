namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class TimingDto : IId
    {        
        public string Id { get; set; }
        public CodeDto Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Window { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public string WindowLower { get; set; }
        public string WindowUpper { get; set; }
        public CodeDto RelativeToFrom { get; set; }
    }
}
