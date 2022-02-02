using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyDesignDTO
    {
        public List<CurrentSectionsDTO> currentSections { get; set; }
        public string studyDesignId { get; set; }
    }
}
