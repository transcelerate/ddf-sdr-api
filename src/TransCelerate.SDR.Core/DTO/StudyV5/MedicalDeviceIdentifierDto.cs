namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class MedicalDeviceIdentifierDto : IdentifierDto
    {

        public CodeDto Type { get; set; }
    }
}
