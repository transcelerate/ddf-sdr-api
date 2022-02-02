using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudyDTO
    {
        //public string _id { get; set; }
        public GetClinicalStudyDTO clinicalStudy { get; set; }
        public AuditTrailDTO auditTrail { get; set; }
    }
}
