using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class StudyProtocolVersionDto : IUuid
    {
        public string Uuid { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public string ProtocolAmendment { get; set; }
        public string ProtocolEffectiveDate { get; set; }
        public List<CodeDto> ProtocolStatus { get; set; }
        public string ProtocolVersion { get; set; }
        public string PublicTitle { get; set; }
        public string ScientificTitle { get; set; }
    }
}
