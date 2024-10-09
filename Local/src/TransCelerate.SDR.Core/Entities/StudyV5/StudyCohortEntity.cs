using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyCohortEntity : PopulationDefinitionEntity
    {        
        public List<CharacteristicEntity> Characteristics { get; set; }   
    }
}
