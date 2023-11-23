﻿namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ResponseCodeEntity : IId
    {       
        public string Id { get; set; }
        public bool IsEnabled { get; set; }
        public CodeEntity Code { get; set; }
    }
}
