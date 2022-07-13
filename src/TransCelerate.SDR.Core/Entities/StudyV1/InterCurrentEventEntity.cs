using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class InterCurrentEventEntity
    {
        public string Uuid { get; set; }
        public string InterCurrentEventDesc { get; set; }
        public string InterCurrentEventName { get; set; }
        public List<CodeEntity> Strategy { get; set; }
    }
}
