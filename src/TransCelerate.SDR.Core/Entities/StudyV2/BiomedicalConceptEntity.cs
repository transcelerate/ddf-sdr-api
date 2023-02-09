using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class BiomedicalConceptEntity : Iid
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
