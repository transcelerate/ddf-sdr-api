﻿namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyDto
    {
        public ClinicalStudyDto ClinicalStudy { get; set; }
        public AuditTrailDto AuditTrail { get; set; }
        public TransCelerate.SDR.Core.DTO.Common.LinksForUIDto Links { get; set; }
    }
}