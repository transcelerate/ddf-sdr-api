

using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class AuditTrailResponseDto
    {
        public string StudyId { get; set; }
        public List<AuditTrailDto> AuditTrail { get; set; } 
    }
}
