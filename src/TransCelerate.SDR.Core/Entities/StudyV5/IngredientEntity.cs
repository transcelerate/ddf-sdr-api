namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class IngredientEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public SubstanceEntity Substance { get; set; }
        public CodeEntity Role { get; set; }
    }
}
