﻿namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyDefinitionsEntity
    {
        public object Id { get; set; }
        public StudyEntity Study { get; set; }
        public AuditTrailEntity AuditTrail { get; set; }
    }
}
