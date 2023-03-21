using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class BiomedicalConceptEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.BiomedicalConceptId)]
        public string Id { get; set; }
        public string BcName { get; set; }
        public List<string> BcSynonyms { get; set; }
        public string BcReference { get; set; }
        public List<BiomedicalConceptPropertyEntity> BcProperties { get; set; }
        public AliasCodeEntity BcConceptCode { get; set; }
    }
}
