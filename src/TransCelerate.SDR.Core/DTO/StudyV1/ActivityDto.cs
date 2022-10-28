using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class ActivityDto : IUuid
    {
        public string Uuid { get; set; }
        public string ActivityDesc { get; set; }
        public string ActivityName { get; set; }
        public List<DefinedProcedureDto> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public List<StudyDataCollectionDto> StudyDataCollection { get; set; }
    }
}
