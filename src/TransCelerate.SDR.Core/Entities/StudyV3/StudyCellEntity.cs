using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyCellEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.StudyCellId)]
        public string Id { get; set; }
        public string StudyArmId { get; set; }
        public string StudyEpochId { get; set; }
        public List<string> StudyElementIds { get; set; }
    }
}
