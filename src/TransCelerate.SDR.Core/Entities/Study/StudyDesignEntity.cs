using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class StudyDesignEntity
    {
        public List<CurrentSectionsEntity> currentSections { get; set; }
        public string studyDesignId { get; set; }
    }
}
