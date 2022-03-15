using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.Core.DTO
{
    /// <summary>
    /// This class is a DTO for POST Method for a study
    /// </summary>
    public class PostStudyDTO
    {
        public ClinicalStudyDTO clinicalStudy { get; set; }
        //public AuditTrailDTO auditTrailDTO { get; set; }
    }
}
