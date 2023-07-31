namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyProtocolVersionDto : IId
    {        
        public string Id { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public string ProtocolAmendment { get; set; }
        public string ProtocolEffectiveDate { get; set; }
        public CodeDto ProtocolStatus { get; set; }
        public string ProtocolVersion { get; set; }
        public string PublicTitle { get; set; }
        public string ScientificTitle { get; set; }
    }
}
