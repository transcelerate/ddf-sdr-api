using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class BiomedicalConceptSurrogateDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.BcSurrogateId)]
        public string Id { get; set; }
        public string BcSurrogateName { get; set; }
        public string BcSurrogateDescription { get; set; }
        public string BcSurrogateReference { get; set; }
    }
}
