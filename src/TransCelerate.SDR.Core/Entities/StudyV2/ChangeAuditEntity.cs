using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
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
        public Object _id { get; set; }
        public ChangeAuditEntity ChangeAudit { get; set; }
    }
}
