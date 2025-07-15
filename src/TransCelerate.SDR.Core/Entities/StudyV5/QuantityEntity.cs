namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class QuantityEntity : QuantityRangeEntity
    {
        public AliasCodeEntity Unit { get; set; }
        public int Value { get; set; }
    }
}
