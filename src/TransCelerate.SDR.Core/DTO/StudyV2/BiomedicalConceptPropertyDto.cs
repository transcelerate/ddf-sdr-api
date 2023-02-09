using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class BiomedicalConceptPropertyDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.BcPropertyId)]
        public string Id { get; set; }
        public string BcPropertyName { get; set; }
        public object BcPropertyRequired { get; set; }
        public object BcPropertyEnabled { get; set; }
        public string BcPropertyDataType { get; set; }
        public List<ResponseCodeDto> BcPropertyResponseCodes { get; set; }
        public AliasCodeDto BcPropertyConceptCode { get; set; }
    }
}
