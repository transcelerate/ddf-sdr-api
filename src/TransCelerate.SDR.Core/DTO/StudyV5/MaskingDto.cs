namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class MaskingDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public bool IsMasked { get; set; }

    }
}
