using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudyAuditDTO
    {    
        public string studyId { get; set; }     
        public List<AuditTrailEndpointResponseDTO> auditTrail { get; set; }
    }

    public class AuditTrailEndpointResponseDTO
    {
        public string studyTag { get; set; }
        public string studyStatus { get; set; }
        public string entryDateTime { get; set; }
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
    }
}
