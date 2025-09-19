namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class DurationEntity : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public QuantityRangeEntity Quantity { get; set; }
        public bool DurationWillVary { get; set; }
        public string ReasonDurationWillVary { get; set; }
    }
}
