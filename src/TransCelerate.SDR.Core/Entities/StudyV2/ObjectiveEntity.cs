using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class ObjectiveEntity : IUuid
    {
        public string Uuid { get; set; }
        public string ObjectiveDescription { get; set; }
        public List<CodeEntity> ObjectiveLevel { get; set; }
        public List<EndpointEntity> ObjectiveEndpoints { get; set; }
    }
}
