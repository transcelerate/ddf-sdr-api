using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ChangeAuditEntity
    {
        public string Study_uuid { get; set; }

        public List<ChangesEntity> Changes { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ChangeAuditStudyEntity
    {
        public ChangeAuditEntity ChangeAudit { get; set; }
    }
}
