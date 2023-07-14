

using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class AuditTrailResponseDto
    {
        public string StudyId { get; set; }
        public List<AuditTrailResponseWithLinksDto> RevisionHistory { get; set; }
    }
}
