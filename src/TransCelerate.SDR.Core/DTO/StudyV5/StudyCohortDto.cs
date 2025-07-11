using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyCohortDto : PopulationDefinitionDto
    {
        public List<CharacteristicDto> Characteristics { get; set; }
        public List<IndicationDto> Indications { get; set; } 
    }
}
