using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyArmEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyArmId)]
        public string Id { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public List<CodeEntity> StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public List<CodeEntity> StudyArmType { get; set; }        
    }
}
