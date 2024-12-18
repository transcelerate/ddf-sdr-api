namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyArmEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.StudyArmId)]
        public string Id { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public CodeEntity StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public CodeEntity StudyArmType { get; set; }
    }
}
