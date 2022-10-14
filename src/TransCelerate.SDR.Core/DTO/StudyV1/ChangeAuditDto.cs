using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
   
    public class ChangeAuditDto
    {
        public string Study_uuid { get; set; }

        public List<ChangesDto> Changes { get; set; }
    }

    public class ChangeAuditStudyDto
    {
        public ChangeAuditDto ChangeAudit { get; set; }
    }
}
