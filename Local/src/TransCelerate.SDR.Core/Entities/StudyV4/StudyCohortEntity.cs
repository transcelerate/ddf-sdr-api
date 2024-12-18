using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class StudyCohortEntity : PopulationDefinitionEntity
    {        
        public List<CharacteristicEntity> Characteristics { get; set; }   
    }
}
