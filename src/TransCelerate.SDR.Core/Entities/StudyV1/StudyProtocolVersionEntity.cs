using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyProtocolVersionEntity : IUuid
    {
        public string Uuid { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public string ProtocolAmendment { get; set; }
        public string ProtocolEffectiveDate { get; set; }
        public List<CodeEntity> ProtocolStatus { get; set; }
        public string ProtocolVersion { get; set; }
        public string PublicTitle { get; set; }
        public string ScientificTitle { get; set; }
    }
}
