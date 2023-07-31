namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class TimingEntity : IId
    {        
        public string Id { get; set; }
        public CodeEntity TimingType { get; set; }
        public string TimingValue { get; set; }
        public string TimingDescription { get; set; }
        public string TimingWindow { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public string TimingWindowLower { get; set; }
        public string TimingWindowUpper { get; set; }
        public CodeEntity TimingRelativeToFrom { get; set; }
    }
}
