using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class IndicationDto : IUuid
    {
        public string Uuid { get; set; }
        public string IndicationDesc { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
