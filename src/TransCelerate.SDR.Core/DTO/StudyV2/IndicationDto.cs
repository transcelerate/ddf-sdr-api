using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class IndicationDto : IUuid
    {
        public string Uuid { get; set; }
        public string IndicationDesc { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
