using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class AdministrableProductEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public List<IngredientEntity> Ingredients { get; set; }
        public List<AdministrableProductPropertyEntity> Properties { get; set; }
        public List<AdministrableProductIdentifierEntity> Identifiers { get; set; }
        public AliasCodeEntity AdministrableDoseForm { get; set; }
        public CodeEntity PharmacologicClass { get; set; }
    }
}
