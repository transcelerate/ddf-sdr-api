using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class ActivityDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<DefinedProcedureDTO> DefinedProcedures { get; set; }
        public List<StudyDataCollectionDTO> StudyDataCollection { get; set; }
    }
}
