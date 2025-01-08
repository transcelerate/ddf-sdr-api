using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ResearchOrganizationEntity : OrganizationEntity
    {                
        public List<StudySiteEntity> Manages { get; set; }
    }
}
