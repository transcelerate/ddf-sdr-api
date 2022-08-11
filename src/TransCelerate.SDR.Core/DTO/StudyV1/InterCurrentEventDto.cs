using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class InterCurrentEventDto
    {
        public string Uuid { get; set; }
        public string InterCurrentEventDesc { get; set; }
        public string InterCurrentEventName { get; set; }
        public List<CodeDto> Strategy { get; set; }
    }
}
