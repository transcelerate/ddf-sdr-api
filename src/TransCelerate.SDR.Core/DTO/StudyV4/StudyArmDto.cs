namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyArmDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeDto Type { get; set; }
        public string DataOriginDescription { get; set; }
        public CodeDto DataOriginType { get; set; }

    }
}
