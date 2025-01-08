using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class AliasCodeDto : IId
    {        
        public string Id { get; set; }
        public CodeDto StandardCode { get; set; }
        public List<CodeDto> StandardCodeAliases { get; set; }
        public string InstanceType { get; set; }
    }
}
