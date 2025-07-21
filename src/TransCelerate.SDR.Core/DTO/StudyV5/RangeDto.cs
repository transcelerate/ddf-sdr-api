namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class RangeDto : QuantityRangeDto
    {      
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public object IsApproximate { get; set; }
    }
}
