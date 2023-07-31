namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class InterCurrentEventEntity : IId
    {        
        public string Id { get; set; }
        public string IntercurrentEventDescription { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
