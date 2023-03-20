namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyIdentifierEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyIdentifierId)]
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganisationEntity StudyIdentifierScope { get; set; }
    }
}
