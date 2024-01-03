using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyProtocolDocumentVersionDto : IId
    {        
        public string Id { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public CodeDto ProtocolStatus { get; set; }
        public string ProtocolVersion { get; set; }
        public string PublicTitle { get; set; }
        public string ScientificTitle { get; set; }
        public List<GovernanceDateDto> DateValues { get; set; }
        public List<NarrativeContentDto> Contents { get; set; }
        public List<string> ChildrenIds { get; set; }
    }
}
