using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ResearchOrganizationDto : OrganizationDto
    {
        public List<StudySiteDto> Manages { get; set; }
    }
}
