namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ReferenceIdentifierEntity : IdentifierEntity
    {
        public CodeEntity Type { get; set; }
    }
}
