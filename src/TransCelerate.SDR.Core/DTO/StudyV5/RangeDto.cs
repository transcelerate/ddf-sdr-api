namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class RangeDto : QuantityRangeDto
    {      
        public QuantityDto MinValue { get; set; }
        public QuantityDto MaxValue { get; set; }
        public object IsApproximate { get; set; }
    }
}
