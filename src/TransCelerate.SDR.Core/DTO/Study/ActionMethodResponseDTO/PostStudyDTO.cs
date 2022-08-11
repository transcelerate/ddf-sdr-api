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
        /// <summary>
        /// This property holds the ClinicalStudy Component of the Study for POST Method
        /// </summary>
        public ClinicalStudyDTO clinicalStudy { get; set; }
        /// <summary>
        /// This property holds the Audit Trail Component of the Study for POST Method
        /// </summary>
        public AuditTrailDTO auditTrail { get; set; }
    }
}
