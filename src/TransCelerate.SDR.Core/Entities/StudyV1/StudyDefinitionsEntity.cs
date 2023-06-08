namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyDefinitionsEntity
    {
        public object Id { get; set; }
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.ParentElement.ClinicalStudy)]
        public StudyEntity Study { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }
}
