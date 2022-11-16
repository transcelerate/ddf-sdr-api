using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class ActivityEntity : IUuid
    {
        public string Uuid { get; set; }
        public string activityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<DefinedProcedureEntity> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public List<StudyDataCollectionEntity> StudyDataCollection { get; set; }
    }
}
