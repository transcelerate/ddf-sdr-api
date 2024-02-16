using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyCellEntity : IId
    {        
        public string Id { get; set; }
        public string ArmId { get; set; }
        public string EpochId { get; set; }
        public List<string> ElementIds { get; set; }
        public string InstanceType { get; set; }
    }
}
