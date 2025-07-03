namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class RangeEntity : QuantityRangeEntity
    {      
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public CodeEntity Unit { get; set; }
        public bool IsApproximate { get; set; }
    }
}
