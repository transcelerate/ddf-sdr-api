namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyTitleEntity : IId
    {        
        public string Id { get; set; }
        public string Text { get; set; }
        public CodeEntity Type { get; set; }
        public string InstanceType { get; set; }
    }
}
