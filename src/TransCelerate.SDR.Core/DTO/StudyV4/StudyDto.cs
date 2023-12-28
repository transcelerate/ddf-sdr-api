using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<StudyVersionDto> Versions { get; set; }
        public StudyProtocolDocumentDto DocumentedBy { get; set; }
    }
}
