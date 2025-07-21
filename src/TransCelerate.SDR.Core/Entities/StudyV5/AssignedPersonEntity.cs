namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AssignedPersonEntity : IId
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public PersonNameEntity PersonName { get; set; }
        public string JobTitle { get; set; }
        public string InstanceType { get; set; }
        public OrganizationEntity Organization { get; set; }
	
	}
}
