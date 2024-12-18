namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class InterCurrentEventEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.IntercurrentEventId)]
        public string Id { get; set; }
        public string IntercurrentEventDescription { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
