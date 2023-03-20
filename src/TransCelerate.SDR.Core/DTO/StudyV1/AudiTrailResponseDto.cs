using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    /// <summary>
    /// This class is a DTO for response of GET Audit Trail Endpoint
    /// </summary>
    public class AudiTrailResponseDto
    {
        public string Uuid { get; set; }

        public List<AuditTrailDto> AuditTrail { get; set; }
    }
}
