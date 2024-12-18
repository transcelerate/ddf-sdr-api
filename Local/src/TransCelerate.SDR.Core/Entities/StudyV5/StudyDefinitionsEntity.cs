namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyDefinitionsEntity
    {
        public object Id { get; set; }
        public string UsdmVersion { get; set; }
        public string SystemName { get; set; }
        public string SystemVersion { get; set; }
        public StudyEntity Study { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }
}
