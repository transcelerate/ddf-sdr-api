using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AdministrationEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public QuantityEntity Dose { get; set; }
        public AliasCodeEntity Frequency { get; set; }
        public AliasCodeEntity Route { get; set; }
        public DurationEntity Duration { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public AdministrableProductEntity AdministrableProduct { get; set; }
        public MedicalDeviceEntity MedicalDevice { get; set; }
    }
}
