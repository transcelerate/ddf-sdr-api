namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyElementEntity : IId
    {        
        public string Id { get; set; }
        public string StudyElementDescription { get; set; }
        public string StudyElementName { get; set; }
        public TransitionRuleEntity TransitionStartRule { get; set; }
        public TransitionRuleEntity TransitionEndRule { get; set; }
    }
}
