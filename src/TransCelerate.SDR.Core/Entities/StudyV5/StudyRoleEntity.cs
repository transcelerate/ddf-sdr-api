using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyRoleEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<string> OrganizationIds { get; set; }
        public CodeEntity Code { get; set; }
        public List<string> AppliesToIds { get; set; }
        public MaskingEntity Masking { get; set; }
        public List<AssignedPersonEntity> AssignedPersons { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
    }
}
