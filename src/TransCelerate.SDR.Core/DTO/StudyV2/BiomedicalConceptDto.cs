using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class BiomedicalConceptDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.BiomedicalConceptId)]
        public string Id { get; set; }
        public string BcName { get; set; }
        public List<string> BcSynonyms { get; set; }
        public string BcReference { get; set; }
        public List<BiomedicalConceptPropertyDto> BcProperties { get; set; }
        public AliasCodeDto BcConceptCode { get; set; }
    }
}
