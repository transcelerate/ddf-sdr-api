using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ActivityDTO
    {
        public string id { get; set; }
        public string description { get; set; }
        public List<DefinedProcedureDTO> definedProcedures { get; set; }
        public List<StudyDataCollectionDTO> studyDataCollection { get; set; }
    }
}
