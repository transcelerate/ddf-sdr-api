namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class QuantityDto : QuantityRangeDto
    {
        public AliasCodeDto Unit { get; set; }
        public object Value { get; set; }
    }
}
