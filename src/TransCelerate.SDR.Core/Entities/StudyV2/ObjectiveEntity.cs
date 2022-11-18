﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ObjectiveEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ObjectiveId)]
        public string Id { get; set; }
        public string ObjectiveDescription { get; set; }
        public List<CodeEntity> ObjectiveLevel { get; set; }
        public List<EndpointEntity> ObjectiveEndpoints { get; set; }
    }
}
