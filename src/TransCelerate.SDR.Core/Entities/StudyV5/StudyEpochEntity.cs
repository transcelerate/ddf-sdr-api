﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyEpochEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public CodeEntity Type { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string InstanceType { get; set; }
		public List<CommentAnnotationEntity> Notes { get; set; }
	}
}
