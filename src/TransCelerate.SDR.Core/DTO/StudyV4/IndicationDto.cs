using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class IndicationDto : IId
    {        
        public string Id { get; set; }
        public string IndicationDescription { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
