using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.Core.DTO
{
    public class PostStudyDTO
    {
        public ClinicalStudyDTO clinicalStudy { get; set; }
        public AuditTrailDTO auditTrailDTO { get; set; }
    }
}
