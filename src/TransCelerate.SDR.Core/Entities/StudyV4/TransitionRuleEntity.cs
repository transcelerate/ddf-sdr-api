﻿namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class TransitionRuleEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }        
        public string Description { get; set; }
        public string Text { get; set; }
        public string InstanceType { get; set; }
    }
}
