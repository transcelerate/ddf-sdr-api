using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyDesignPopulationDto : PopulationDefinitionDto
    {        
        public List<StudyCohortDto> Cohorts { get; set; }
    }
}
