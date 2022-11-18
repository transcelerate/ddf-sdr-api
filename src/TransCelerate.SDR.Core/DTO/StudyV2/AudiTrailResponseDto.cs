﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    /// <summary>
    /// This class is a DTO for response of GET Audit Trail Endpoint
    /// </summary>
    public class AudiTrailResponseDto
    {
        public string StudyId { get; set; }
        
        public List<AuditTrailDto> AuditTrail { get; set; }
    }    
}
