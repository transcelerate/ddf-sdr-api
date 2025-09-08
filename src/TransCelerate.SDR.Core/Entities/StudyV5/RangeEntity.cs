namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class RangeEntity : QuantityRangeEntity
    {      
        public QuantityEntity MinValue { get; set; }
        public QuantityEntity MaxValue { get; set; }
        public bool IsApproximate { get; set; }
    }
}
