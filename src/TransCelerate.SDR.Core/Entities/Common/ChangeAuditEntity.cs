using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ChangeAuditEntity
    {
        public string StudyId { get; set; }

        public List<ChangesEntity> Changes { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ChangeAuditStudyEntity
    {
        public object Id { get; set; }
        public ChangeAuditEntity ChangeAudit { get; set; }
    }
}
