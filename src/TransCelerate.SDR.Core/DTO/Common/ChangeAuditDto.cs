using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Common
{

    public class ChangeAuditDto
    {
        public string StudyId { get; set; }

        public List<ChangesDto> Changes { get; set; }
    }

    public class ChangeAuditStudyDto
    {
        public ChangeAuditDto ChangeAudit { get; set; }
    }
}
