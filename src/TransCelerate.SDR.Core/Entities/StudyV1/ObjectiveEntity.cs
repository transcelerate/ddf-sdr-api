using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class ObjectiveEntity : IUuid
    {
        public string Uuid { get; set; }
        public string ObjectiveDesc { get; set; }
        public List<CodeEntity> ObjectiveLevel { get; set; }
        public List<EndpointEntity> ObjectiveEndpoints { get; set; }
    }
}
