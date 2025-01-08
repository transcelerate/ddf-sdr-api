namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyDefinitionsDto
    {
        public StudyDto Study { get; set; }
        public string UsdmVersion { get; set; }
        public string SystemName { get; set; }
        public string SystemVersion { get; set; }
        public AuditTrailDto AuditTrail { get; set; }
        public TransCelerate.SDR.Core.DTO.Common.LinksForUIDto Links { get; set; }
    }
}
