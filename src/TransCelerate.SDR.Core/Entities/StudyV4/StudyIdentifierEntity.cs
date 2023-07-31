namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyIdentifierEntity : IId
    {        
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganisationEntity StudyIdentifierScope { get; set; }
    }
}
