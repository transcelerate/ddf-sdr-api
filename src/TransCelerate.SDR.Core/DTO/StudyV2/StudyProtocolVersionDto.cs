using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyProtocolVersionDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyProtocolVersionId)]
        public string Id { get; set; }
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
