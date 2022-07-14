using System;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyEntity
    {
        public Object _id { get; set; }
        public ClinicalStudyEntity ClinicalStudy { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }
}
