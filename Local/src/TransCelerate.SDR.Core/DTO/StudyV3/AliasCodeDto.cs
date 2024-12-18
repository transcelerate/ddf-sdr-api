using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class AliasCodeDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.AliasCodeId)]
        public string Id { get; set; }
        public CodeDto StandardCode { get; set; }
        public List<CodeDto> StandardCodeAliases { get; set; }
    }
}
