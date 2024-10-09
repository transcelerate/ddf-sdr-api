namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class TransitionRuleEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.TransitionRuleId)]
        public string Id { get; set; }
        public string TransitionRuleDescription { get; set; }
    }
}
