using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class BiomedicalConceptPropertyEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.BcPropertyId)]
        public string Id { get; set; }
        public string BcPropertyName { get; set; }
        public bool BcPropertyRequired { get; set; }
        public bool BcPropertyEnabled { get; set; }
        public string BcPropertyDataType { get; set; }
        public List<ResponseCodeEntity> BcPropertyResponseCodes { get; set; }
        public AliasCodeEntity BcPropertyConceptCode { get; set; }
    }
}
