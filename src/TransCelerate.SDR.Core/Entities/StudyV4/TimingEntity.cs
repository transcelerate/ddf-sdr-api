namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class TimingEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public CodeEntity Type { get; set; }
        public string Value { get; set; }
        public string ValueLabel { get; set; }
        public string Description { get; set; }
        public string WindowLabel { get; set; }
        public string RelativeToScheduledInstanceId { get; set; }
        public string RelativeFromScheduledInstanceId { get; set; }
        public string WindowLower { get; set; }
        public string WindowUpper { get; set; }
        public CodeEntity RelativeToFrom { get; set; }
        public string InstanceType { get; set; }
    }
}
