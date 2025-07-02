namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class IntercurrentEventEntity : SyntaxTemplateEntity
    {
        public string Strategy { get; set; }
	}
}
