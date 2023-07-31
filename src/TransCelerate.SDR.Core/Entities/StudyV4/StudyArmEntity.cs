namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyArmEntity : IId
    {        
        public string Id { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public CodeEntity StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public CodeEntity StudyArmType { get; set; }
    }
}
