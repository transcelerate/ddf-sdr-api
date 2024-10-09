namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class MaskingDto : IId
    {        
        public string Id { get; set; }
        public string Description { get; set; }
        public CodeDto Role { get; set; }
        public string InstanceType { get; set; }

    }
}
