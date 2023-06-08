namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDefinitionsDto
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.ParentElement.ClinicalStudy)]
        public StudyDto Study { get; set; }
        public AuditTrailDto AuditTrail { get; set; }
        public TransCelerate.SDR.Core.DTO.Common.LinksForUIDto Links { get; set; }
    }
}
