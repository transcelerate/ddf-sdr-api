using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyCohortDto : PopulationDefinitionDto
    {        
        public List<CharacteristicDto> Characteristics { get; set; }   
    }
}
