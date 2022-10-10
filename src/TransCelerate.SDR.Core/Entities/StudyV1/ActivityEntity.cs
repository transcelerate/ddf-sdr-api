using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class ActivityEntity : IUuid
    {
        public string Uuid { get; set; }
        public string ActivityDesc { get; set; }
        public string ActivityName { get; set; }
        public List<DefinedProcedureEntity> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public List<StudyDataCollectionEntity> StudyDataCollection { get; set; }
    }
}
