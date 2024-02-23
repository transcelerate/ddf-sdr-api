using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EncounterEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeEntity> ContactModes { get; set; }        
        public List<CodeEntity> EnvironmentalSetting { get; set; }
        public CodeEntity Type { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public string ScheduledAtTimingId { get; set; }
        public TransitionRuleEntity TransitionStartRule { get; set; }
        public TransitionRuleEntity TransitionEndRule { get; set; }
        public string InstanceType { get; set; }
    }
}
