namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class TransitionRuleEntity : IId
    {        
        public string Id { get; set; }
        public string TransitionRuleDescription { get; set; }
    }
}
