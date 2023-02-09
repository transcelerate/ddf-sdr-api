using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ResponseCodeDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ResponseCodeId)]
        public string Id { get; set; }
        public object ResponseCodeEnabled { get; set; }
        public CodeDto Code { get; set; }
    }
}
